﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chloe.DbExpressions;
using Chloe.InternalExtensions;

namespace Chloe.Oracle.MethodHandlers
{
    class NextValueForSequence_Handler : IMethodHandler
    {
        public bool CanProcess(DbMethodCallExpression exp)
        {
            if (exp.Method.DeclaringType != UtilConstants.TypeOfSql)
                return false;

            return true;
        }
        public void Process(DbMethodCallExpression exp, SqlGenerator generator)
        {
            /* SELECT "SYSTEM"."USERS_AUTOID"."NEXTVAL" FROM "DUAL" */

            string sequenceName = (string)exp.Arguments[0].Evaluate();
            if (string.IsNullOrEmpty(sequenceName))
                throw new ArgumentException("The sequence name cannot be empty.");

            string sequenceSchema = (string)exp.Arguments[1].Evaluate();

            if (!string.IsNullOrEmpty(sequenceSchema))
            {
                generator.QuoteName(sequenceSchema);
                generator.SqlBuilder.Append(".");
            }

            generator.QuoteName(sequenceName);
            generator.SqlBuilder.Append(".nextval");
        }
    }
}
