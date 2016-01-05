﻿using System;
using Odin.Attributes;
using Odin.Tests.Lib;

namespace Odin.Tests.Parsing
{
    public enum Numbers
    {
        One,
        Two,
        Three
    };

    public class ArgumentTypesCommand : Command
    {
        [Action]
        public void WithBoolean(bool input)
        {
            
        }

        [Action]
        public void WithNullableBoolean(bool? input)
        {

        }

        [Action]
        public void WithInt32(int input)
        {
        }

        [Action]
        public void WithNullableInt32(int? input)
        {
        }

        [Action]
        public void WithInt64(long input)
        {
        }

        [Action]
        public void WithNullableInt64(long? input)
        {
        }

        [Action]
        public void WithDouble(double input)
        {
        }
        [Action]
        public void WithNullableDouble(double? input)
        {
        }

        [Action]
        public void WithDecimal(double input)
        {
        }
        [Action]
        public void WithNullableDecimal(decimal? input)
        {
        }
        [Action]
        public void WithEnum(Numbers input)
        {   
        }
        [Action]
        public void WithNullableEnum(Numbers? input)
        {
        }

        [Action]
        public void WithDateTime(DateTime input)
        {
        }

        [Action]
        public void WithNullableDateTime(DateTime? input)
        {
        }

        [Action]
        public void WithBooleanYesNoParser(
            [Alias("i")]
            [Parser(typeof(YesNoParser))] bool input, 
            int input2, string input3)
        { 
        

        }

        [Action]
        public void WithStringArray(string[] fileNames, int someOtherInput)
        {
            
        }

        [Action]
        public void WithBoolArray(bool[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithNullableBoolArray(bool?[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithInt32Array(int[] numbers, int someOtherInput)
        {
            
        }

        [Action]
        public void WithNullableInt32Array(int?[] numbers, int someOtherInput)
        {

        }

        [Action]
        public void WithInt64Array(long[] numbers, int someOtherInput)
        {

        }

        [Action]
        public void WithNullableInt64Array(long?[] numbers, int someOtherInput)
        {

        }

        [Action]
        public void WithDateTimeArray(DateTime[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithNullableDateTimeArray(DateTime?[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithDecimalArray(decimal[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithNullableDecimalArray(decimal?[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithDoubleArray(double[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithNullableDoubleArray(double?[] values, int someOtherInput)
        {

        }

        [Action]
        public void WithEnumArray(Numbers[] values, int someOtherInput)
        {

        }


    }
}
