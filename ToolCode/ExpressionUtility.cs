using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToolCode
{
    public class ExpressionUtility
    {
        /// <summary>
        /// 利用输入类的属性与输入容器制作( == ||)关系的表达式树
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="inputProperty"></param>
        /// <param name="inputContainsSet"></param>
        /// <returns></returns>
        public static Func<TClass, bool> GetEqulaOrExpression<TClass, TProperty>(Func<TClass, TProperty> inputProperty, HashSet<TProperty> inputContainsSet)
        {
            //获取输入的参数
            ParameterExpression useInputExpression = Expression.Parameter(typeof(TClass));

            var tempMethod = inputProperty.Method;

            var useValue = Expression.Call(tempMethod,useInputExpression);

            List<ConstantExpression> lstUseConstrant = new List<ConstantExpression>();

            foreach (var oneValue in inputContainsSet)
            {
                lstUseConstrant.Add(Expression.Constant(oneValue, typeof(TProperty)));
            }

            Expression returnExpression = Expression.Constant(false, typeof(bool));

            //表达组合
            foreach (var oneExpression in lstUseConstrant)
            {
                returnExpression = Expression.Or(returnExpression, Expression.Equal(useValue, oneExpression));
            }


            return Expression.Lambda<Func<TClass, bool>>(returnExpression,useInputExpression).Compile();
        }
    }
}
