using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagerLibrary
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_maxValue = 0;

        public float MaxValue
        {
            get { return r_maxValue; }
        }

        private readonly float r_minValue = 0;

        public float MinValue
        {
            get { return r_minValue; }
        }

        public ValueOutOfRangeException(float i_MinimumValue, float i_MaximumValue, float i_InputValue, string i_ValueName)
            : base(string.Format("The range of {3} is: {0} - {1}, and got {2}", i_MinimumValue, i_MaximumValue, i_InputValue, i_ValueName))

        {
            r_minValue = i_MinimumValue;
            r_maxValue = i_MaximumValue;
        }
    }
}
