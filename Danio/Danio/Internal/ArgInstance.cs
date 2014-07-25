namespace Ruibm.Danio.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public class ArgInstance
    {
        public ArgInstance(ArgAttribute arg, FieldInfo field, Type parentType)
            : this(arg, field, parentType, parentType)
        {
        }
        public ArgInstance(ArgAttribute arg, FieldInfo field, Type parentType, object parentObject)
            : this(arg, field, parentType, parentObject, CreateSystemDefaultValue(field.FieldType))
        {
        }

        private static object CreateSystemDefaultValue(Type type)
        {
           if(type.IsValueType)
           {
              return Activator.CreateInstance(type);
           }
           return null;
        }

        public ArgInstance(ArgAttribute arg, FieldInfo field, Type parentType, object parentObject, object defaultValue)
        {
            ParentType = parentType;
            Field = field;
            Arg = arg;
            ParentObject = parentObject;
            DefaultValue = defaultValue;
        }

        public object DefaultValue { get; private set; }

        public object ParentObject { get; private set; }

        public Type ParentType { get; private set; }

        public FieldInfo Field { get; private set; }

        public ArgAttribute Arg { get; private set; }

        public string FullName
        {
            get
            {
                return ParentType.FullName + "." + Field.Name;
            }
        }

        public string Name
        {
            get
            {
                return Field.Name;
            }
        }

        public string ShortName
        {
            get
            {
                if (Arg.ShortName == null)
                {
                    return null;
                }
                else
                {
                    return Arg.ShortName.ToString();
                }
            }
        }
    }
}
