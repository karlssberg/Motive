using System;

namespace Test
{
    public static partial class Factory
    {
        public static Step_0 Number(Int32 number)
        {
            return new Step_0(number);
        }
    }

    public struct Step_0
    {
        private readonly Int32 _number__parameter;
        public Step_0(Int32 number)
        {
            _number__parameter = number;
        }

        public Step_1 Text(String text)
        {
            return new Step_1(_number__parameter, text);
        }
    }

    public struct Step_1
    {
        private readonly Int32 _number__parameter;
        private readonly String _text__parameter;
        public Step_1(Int32 number, String text)
        {
            _number__parameter = number;
            _text__parameter = text;
        }

        public MyBuildTarget Id(Guid id)
        {
            return new MyBuildTarget(_number__parameter, _text__parameter, id);
        }
    }
}
