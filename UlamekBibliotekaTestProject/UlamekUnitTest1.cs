using Microsoft.VisualStudio.TestTools.UnitTesting;
using UlamekBiblioteka;
using System;

namespace UlamekBibliotekaTestProject
{
    [TestClass]
    public class UlamekUnitTest1
    {
        [TestMethod]
        public void TestKonstruktoraWlasnosci()
        {
            int licznik = 1;
            int mianownik = 2;

            Ulamek u = new Ulamek(licznik, mianownik);

            PrivateObject po = new PrivateObject(u);
            int u_licznik = u.Licznik;
            int u_mianownik = (int)po.GetField("mianownik");

            Assert.AreEqual(licznik, u_licznik, "Niezgodnosc w liczniku");
            Assert.AreEqual(mianownik, u_mianownik, "Niezgodnosc w mianowniku");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestKonstrukotraWyjatek()
        {
            Ulamek u = new Ulamek(1, 0);
        }
        [TestMethod]
        public void TestUprosc()
        {
            Ulamek u = new Ulamek(4, -2);

            u.Uprosc();

            Assert.AreEqual(-2, u.Licznik);
            Assert.AreEqual(1, u.Mianownik);
        }
        [TestMethod]
        public void TestOperatorow()
        {
            Ulamek a = Ulamek.Polowa;
            Ulamek b = Ulamek.Cwierc;

            Assert.AreEqual(new Ulamek(3, 4), a + b, "Niepowodzenie przy sumie");
            Assert.AreEqual(new Ulamek(1, 4), a - b, "Niepowodzenie przy roznicy");
            Assert.AreEqual(new Ulamek(1, 8), a * b, "Niepowodzenie przy iloczynie");
            Assert.AreEqual(new Ulamek(2), a / b, "Niepowodzenie przy ilorazie");
        }

        Random r = new Random();

        private int LosujLiczbeCalkowita(int? MaxVal = null)
        {
            if (!MaxVal.HasValue)
                return r.Next(int.MinValue, int.MaxValue);
            else
            {
                MaxVal = Math.Abs(MaxVal.Value);
                return r.Next(-MaxVal.Value, MaxVal.Value);
            }
        }

        private int LosujLiczbeCalkowitaRoznaZero(int? MaxVal = null)
        {
            int wartosc;
            do
            {
                wartosc = LosujLiczbeCalkowita(MaxVal);
            }
            while (wartosc == 0);
            return wartosc;
        }

        [TestMethod]
        public void TestSortowania()
        {
            for (int j = 0; j < 100; ++j)
            {
                Ulamek[] tablica = new Ulamek[100];
                for (int i = 0; i < tablica.Length; ++i)
                    tablica[i] = new Ulamek(LosujLiczbeCalkowita(), LosujLiczbeCalkowitaRoznaZero());

                Array.Sort(tablica);

                bool tablicaPosortowana = true;

                for (int i = 0; i < tablica.Length - 1; ++i)
                    if (tablica[i] > tablica[i + 1]) tablicaPosortowana = false;
                Assert.IsTrue(tablicaPosortowana);
            }
        }

        [TestMethod]
        public void TestUproscLosowy()
        {
            for (int j = 0; j < 100; ++j)
            {
                Ulamek u = new Ulamek(LosujLiczbeCalkowita(), LosujLiczbeCalkowitaRoznaZero());
                Ulamek kopia = u;

                u.Uprosc();

                Assert.IsTrue(u.Mianownik > 0);
                Assert.AreEqual(kopia.ToDouble(), u.ToDouble());
            }
        }
    }
}
