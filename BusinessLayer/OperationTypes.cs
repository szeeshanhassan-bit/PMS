using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BusinessLayer
{    
    class OperationTypesConverter
	{
        public static OperationTypes ConvertToAccessRight(int i)
        {
            string name = System.Enum.GetName(typeof(OperationTypes), i);

            if (!string.IsNullOrEmpty(name))
            {
                return (OperationTypes)System.Enum.Parse(typeof(OperationTypes), name);
            }

            return OperationTypes.NotDefined;
        }
	}
     
    public enum OperationTypes
    {
        NotDefined = -1,
        Add = 1,
        Update = 2,
        Delete = 3,
        GetAll = 4,
        GetSpecific = 5

    }
}
