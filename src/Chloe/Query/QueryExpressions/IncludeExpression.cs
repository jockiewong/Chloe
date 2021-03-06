﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chloe.Query.QueryExpressions
{
    class IncludeExpression : QueryExpression
    {
        public IncludeExpression(Type elementType, QueryExpression prevExpression, NavigationNode navigationNode)
           : base(QueryExpressionType.Include, elementType, prevExpression)
        {
            this.NavigationNode = navigationNode;
        }
        public NavigationNode NavigationNode { get; private set; }

        public override T Accept<T>(QueryExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }

    public class NavigationNode
    {
        public NavigationNode()
        {
        }
        public NavigationNode(PropertyInfo property)
        {
            this.Property = property;
        }
        public PropertyInfo Property { get; set; }
        public LambdaExpression Condition { get; set; }
        public LambdaExpression Filter { get; set; }
        public NavigationNode Next { get; set; }

        public NavigationNode Clone()
        {
            NavigationNode current = new NavigationNode(this.Property) { Condition = this.Condition, Filter = this.Filter };

            if (this.Next != null)
            {
                current.Next = current.Clone();
            }

            return current;
        }

        public NavigationNode GetLast()
        {
            if (this.Next == null)
                return this;

            return this.Next.GetLast();
        }
    }
}
