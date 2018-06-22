using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ToolCode
{
    /// <summary>
    /// 表达式树工具
    /// </summary>
    public class ExpressionUtility
    {

        /// <summary>
        /// 利用输入类的属性与输入容器制作表达式树
        /// </summary>
        /// <typeparam name="TClass">使用的类泛型</typeparam>
        /// <typeparam name="TProperty">使用的类属性泛型</typeparam>
        /// <param name="inputProperty">使用的获取属性的Func</param>
        /// <param name="inputContainsSet">使用的容器</param>
        /// <param name="inputComparer">使用的比较Func 第一个参数为从输入中取出的值，第二个参数为从容器中取出的值</param>
        /// <returns>使用的表达式树</returns>
        public static Func<TClass, bool> GetEqulaOrExpression<TClass, TProperty>
            (Func<TClass, TProperty> inputProperty, IEnumerable<TProperty> inputContainsSet, Func<TProperty, TProperty, bool> inputComparer = null)
        {
            if (null == inputComparer)
            {
                inputComparer = GetDefaultFunc<TProperty>();
            }

            //获取输入的参数
            ParameterExpression useInputExpression = Expression.Parameter(typeof(TClass));

            var tempMethod = inputProperty.Method;

            var tempComparerMethod = inputComparer.Method;

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
                returnExpression = Expression.Or(returnExpression, Expression.Call(tempComparerMethod, useValue, oneExpression));
            }


            return Expression.Lambda<Func<TClass, bool>>(returnExpression,useInputExpression).Compile();
        }

        /// <summary>
        /// 使用的默认比较器
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        private static Func<TProperty, TProperty, bool> GetDefaultFunc<TProperty>()
        {
            return (k1, k2) => k1.Equals(k2);
        }
    }
}
