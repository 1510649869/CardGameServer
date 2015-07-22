using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src_dao
{
 
    public class DeploymentGridModel
    {
        public int cardId;//对应的英雄卡牌的id
        //1
        public int swordId;
        //2
        public int hatId;
        //3
        public int clothId;
        //4
        public int bookId;
        //5
        public int shooeId;
        //6
        public int horseId;
        public DeploymentGridModel()
        {
            cardId = -1;
            swordId = -1;
            hatId = -1;
            clothId =- 1;
            bookId = -1;
            shooeId = -1;
            horseId = -1;
        }
    }
}