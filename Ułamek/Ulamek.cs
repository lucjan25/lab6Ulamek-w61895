using System;

namespace UlamekBiblioteka
{
    public struct Ulamek : IComparable<Ulamek>, IConvertible
    {
        private int mianownik;

        public Ulamek(int licznik, int mianownik = 1) : this()
        {
            this.Licznik = licznik;
            this.Mianownik = mianownik;
        }

        public static readonly Ulamek Zero = new Ulamek(0);
        public static readonly Ulamek Jeden = new Ulamek(1);
        public static readonly Ulamek Polowa = new Ulamek(1,2);
        public static readonly Ulamek Cwierc = new Ulamek(1,4);

        public static string Info()
        {
            return "Struktura ulamek";
        }
        
        public override string ToString()
        {
            return Licznik.ToString() + " / " + mianownik.ToString();
        }

        public readonly double ToDouble()
        {
            return Licznik / (double)mianownik;
        }
        #region Convertible
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            if (Licznik != 0)
                return true;
            else
                return false;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(ToDouble());
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(ToDouble());

        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(ToDouble());
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(ToDouble());
        }

        public double ToDouble(IFormatProvider provider)
        {
            return ToDouble();
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(ToDouble());
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(ToDouble());
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(ToDouble());
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(ToDouble());
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(ToDouble());
        }

        public string ToString(IFormatProvider provider)
        {
            return string.Format("{0} / {1}", Licznik, mianownik);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(ToDouble(), conversionType);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(ToDouble());
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(ToDouble());
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(ToDouble());
        }
        #endregion

        public void Uprosc()
        {
            int a = Licznik;
            int b = mianownik;

            //NWD
            int c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            Licznik /= a;
            mianownik /= a;

            if (Math.Sign(Licznik) * Math.Sign(mianownik) < 0)
            {
                Licznik = -Math.Abs(Licznik);
                mianownik = Math.Abs(mianownik);
            }
            else 
            {
                Licznik = Math.Abs(Licznik);
                mianownik = Math.Abs(mianownik);
            }
        }

        #region Wlasnosc

        public int Licznik { get; set; }
        public int Mianownik 
        { 
            get => mianownik;
            set
            {
                if (value == 0)
                    throw new ArgumentException("Mianownik musi byc rozny od zera!");
                mianownik = value;
            }
        }
        #endregion

        #region Operatory arytmetyczne
        public static Ulamek operator -(Ulamek u)
        {
            return new Ulamek(-u.Licznik, u.mianownik);
        }

        public static Ulamek operator +(Ulamek u)
        {
            return u;
        }
        public static Ulamek operator +(Ulamek u1, Ulamek u2)
        {
            Ulamek wynik = new Ulamek(u1.Licznik * u2.mianownik + u2.Licznik * u1.mianownik, u1.mianownik * u2.mianownik);
            wynik.Uprosc();
            return wynik;
        }

        public static Ulamek operator -(Ulamek u1, Ulamek u2)
        {
            Ulamek wynik = new Ulamek(u1.Licznik * u2.mianownik - u2.Licznik * u1.mianownik, u1.mianownik * u2.mianownik);
            wynik.Uprosc();
            return wynik;
        }
        public static Ulamek operator *(Ulamek u1, Ulamek u2)
        {
            Ulamek wynik = new Ulamek(u1.Licznik * u2.Licznik, u1.mianownik * u2.mianownik);
            wynik.Uprosc();
            return wynik;
        }
        public static Ulamek operator /(Ulamek u1, Ulamek u2)
        {
            Ulamek wynik = new Ulamek(u1.Licznik * u2.mianownik, u1.mianownik * u2.Licznik);
            wynik.Uprosc();
            return wynik;
        }
        #endregion

        #region Operatory porownania
        public static bool operator ==(Ulamek u1, Ulamek u2)
        {
            return (u1.ToDouble() == u2.ToDouble());
        }
        public static bool operator !=(Ulamek u1, Ulamek u2)
        {
            return !(u1==u2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Ulamek)) return false;
            Ulamek u = (Ulamek)obj;
            return (this == u);
        }
        public override int GetHashCode()
        {
            return Licznik ^ mianownik;
        }

        public int CompareTo(Ulamek other)
        {
            double roznica = this.ToDouble() - other.ToDouble();
            if (roznica != 0) roznica /= Math.Abs(roznica);
            return (int)roznica;
        }

        
        public static bool operator >(Ulamek u1, Ulamek u2)
        {
            return (u1.ToDouble() > u2.ToDouble());
        }
        public static bool operator >=(Ulamek u1, Ulamek u2)
        {
            return (u1.ToDouble() >= u2.ToDouble());
        }
        public static bool operator <(Ulamek u1, Ulamek u2)
        {
            return (u1.ToDouble() < u2.ToDouble());
        }
        public static bool operator <=(Ulamek u1, Ulamek u2)
        {
            return (u1.ToDouble() <= u2.ToDouble());
        }
        #endregion

        #region Operatory konwersji
        public static explicit operator double(Ulamek u)
        {
            return u.ToDouble();
        }

        public static implicit operator Ulamek(int n)
        {
            return new Ulamek(n);
        }
        #endregion
    }
}
