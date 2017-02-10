using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntitiesLayer;

namespace TestUnit1
{
    [TestClass]
    public class UnitTest1
    {
        //TEST DE CONNEXION

        [TestMethod]
        public void testcheckconnexionuser()
        {
            BusinessLayer.BusinessManager bm = new BusinessLayer.BusinessManager();
            bool result = bm.CheckConnexionUser("toto", "pouet");

            Assert.AreEqual(true, result);

            result = bm.CheckConnexionUser("", "");

            Assert.AreEqual(true, result);
        }


        [TestMethod]
        public void TestGetUtilisateurByLogin()
        {
            StubDataAccessLayer.DalManager bm = new StubDataAccessLayer.DalManager();

            var res = bm.GetUtilisateurByLogin("Admin");
            Utilisateur JeanKev = new Utilisateur("Kev", "Jean", "Admin", "Admin");
            Utilisateur Kevin = new Utilisateur("Kevin", "J", "Adm", "Admin");

            Assert.AreEqual("Jean", res.Prenom);
            Assert.AreNotEqual(Kevin, res);
        }


        //TESTS EN RAPPORT AVEC LES POKEMONS

        [TestMethod]
        public void TestgetPokemonByElem()
        {
            BusinessLayer.BusinessManager bm = new BusinessLayer.BusinessManager();

            var liste = bm.getPokemonByElem(EntitiesLayer.ETypeElement.Feu);

            Assert.AreEqual(2, liste.Count);

            liste = bm.getPokemonByElem(EntitiesLayer.ETypeElement.Eau);
            Assert.AreNotEqual(0, liste.Count);
        }


        [TestMethod]
        public void TestgetPokemonByID()
        {
            BusinessLayer.BusinessManager dm = new BusinessLayer.BusinessManager();

            var res = dm.getPokemonByID(3);

            // Assert.AreEqual(Florizare, res);
            Assert.AreEqual(dm.getPokemonByName("Leviator"), res);
        }


        [TestMethod]
        public void TestgetPokemonByName()
        {
            BusinessLayer.BusinessManager bm = new BusinessLayer.BusinessManager();
            EntitiesLayer.Pokemon Dracofeu = new EntitiesLayer.Pokemon("Dracofeu", ETypeElement.Feu, 130, 60, 30, 45, 35);
            EntitiesLayer.Pokemon pikachu = new EntitiesLayer.Pokemon("Pikachu", ETypeElement.Electrique, 100, 25, 38, 18, 40);
            var res = bm.getPokemonByName("Dracofeu");
            
            //Assert.AreEqual(Dracofeu, res);
            Assert.AreNotEqual(pikachu, res);
        }

        ///////////////////////////////////////////////
        //TESTS STADE, MATCH, ETC...

        [TestMethod]
        public void TestgetStade()
        {
            BusinessLayer.BusinessManager bm = new BusinessLayer.BusinessManager();

            var res = bm.getStade();

            Assert.AreEqual(1, res.Count);
        }


        [TestMethod]
        public void TestgetMatch()
        {
            BusinessLayer.BusinessManager bm = new BusinessLayer.BusinessManager();

            var res = bm.getMatch();
            
            Assert.AreEqual(2, res.Count);
        }


        [TestMethod]
        public void TestgetTournoi()
        {
            BusinessLayer.BusinessManager bm = new BusinessLayer.BusinessManager();

            var res = bm.getTournoi();

            Assert.AreEqual(1, res.Count);
        }
    }
}
