﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.Xna.Framework.Design
{
    public class Vector4IntTypeConverter : TypeConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (VectorIntConversion.CanConvertTo(context, destinationType))
                return true;
            if (destinationType == typeof(string))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            var vec = (Vector4Int)value;

            if (VectorIntConversion.CanConvertTo(context, destinationType))
            {
                return VectorIntConversion.ConvertToFromVector4(context, culture, vec, destinationType);
            }

            if (destinationType == typeof(string))
            {
                var terms = new string[4];
                terms[0] = vec.X.ToString("R", culture);
                terms[1] = vec.Y.ToString("R", culture);
                terms[2] = vec.Z.ToString("R", culture);
                terms[3] = vec.W.ToString("R", culture);

                return string.Join(culture.TextInfo.ListSeparator + " ", terms);
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var sourceType = value.GetType();
            var vec = Vector4Int.Zero;

            if (sourceType == typeof(string))
            {
                var str = (string)value;
                var words = str.Split(culture.TextInfo.ListSeparator.ToCharArray());

                vec.X = int.Parse(words[0], culture);
                vec.Y = int.Parse(words[1], culture);
                vec.Z = int.Parse(words[2], culture);
                vec.W = int.Parse(words[3], culture);

                return vec;
            }

            return base.ConvertFrom(context, culture, value);
        }
    }
}
