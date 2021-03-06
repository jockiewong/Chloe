﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Chloe.Mapper.Binders
{
    public class MemberBinder : IValueSetter
    {
        Action<object, object> _setter;
        IObjectActivator _activtor;
        public MemberBinder(Action<object, object> setter, IObjectActivator activtor)
        {
            this._setter = setter;
            this._activtor = activtor;
        }
        public virtual void SetValue(object obj, IDataReader reader)
        {
            object val = this._activtor.CreateInstance(reader);
            this._setter(obj, val);
        }
    }
}
