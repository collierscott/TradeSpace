using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Assets.Scripts.Data;

namespace Assets.Scripts
{
    public static class MemoHelper
    {
        public static void Update(this List<MemoAsteroid> items, string name, long quantity)
        {
            MemoAsteroid ast = items.Where(i => i.Name == name).FirstOrDefault();
            if (ast == null)
            {
                ast = new MemoAsteroid();
                ast.Name = name;
                ast.Quantity = quantity.Encrypt();
                items.Add(ast);
            }
            else
                ast.Quantity = quantity.Encrypt();
        }

        //public static long FindQuantity(this List<MemoAsteroid> items, Asteroid ast)
        //{
        //    MemoAsteroid item = items.Where(i => i.Name == ast.Name).FirstOrDefault();

        //    //return item == null ? ast.Quantity : item.Quantity.Long;
        //}
    }
}
