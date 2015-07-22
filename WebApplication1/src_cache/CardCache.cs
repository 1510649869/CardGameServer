using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using card;
public class CardCache
{
    private Dictionary<int, Dictionary<int, Card>> userCard = new Dictionary<int, Dictionary<int, Card>>();
    public Card add(int userId, int cardCode)//往数据库中添加装备
    {
        Card card = new Card();
        card.user_id = userId;
        card.gd_code = cardCode;
        card.level = 1;
        card.exp = 0;
        card.additional = 0;
        card.Add();
		userCard[userId].Add ( card.id, card );
        return card;
    }
    public Card[] getList(int userId)//通过登录用户ID获取卡牌列表
    {
        initCache(userId);
        return userCard[userId].Values.ToArray<Card>();
    }
    private void initCache(int userId)
    {
        if (userCard.ContainsKey(userId)) return;
        Card[] cards = Card.GetList(userId);
        Dictionary<int, Card> map = new Dictionary<int, Card>();
        foreach(Card card in cards)
        {
            map.Add(card.id, card);
        }
        userCard.Add(userId, map);
    }


    public Card getCard(int userId, int id)
    {
        initCache(userId);
        return userCard[userId][id];
    }
}
