using Microsoft.VisualStudio.TestTools.UnitTesting;
using LigalFrontend.DAL;
using LigalFrontend.ViewModels;
using System.Collections.Generic;
using LigalFrontend.Models;

namespace LigalFrontend.Tests.DAL
{
    /// <summary>
    /// Descripción resumida de PuntosRecogidaRepoTest
    /// </summary>
    [TestClass]
    public class PuntosRecogidaRepoTest
    {
        private PuntosRecogidaRepo prepo;
        private LigalEntities context;

        public PuntosRecogidaRepoTest()
        {
            prepo = new PuntosRecogidaRepo();
            context = new LigalEntities();
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        //
        // Puede usar los siguientes atributos adicionales conforme escribe las pruebas:
        //
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void getByIdTest()
        {
            int id = 1;
            PuntosRecogidaVM zz = prepo.getById(id);
        }

        [TestMethod]
        public void getByCodigoPTest()
        {
            string codigoP = "C9";
            List<PuntosRecogidaVM> zz = (List<PuntosRecogidaVM>) prepo.getByCodigoP(codigoP);
        }

        [TestMethod]
        public void getByNombrePTest()
        {
            string nombreP = "AFRICOR SANTA COMBA";
            List<PuntosRecogidaVM> zz = (List<PuntosRecogidaVM>)prepo.getByNombreP(nombreP);
        }

        [TestMethod]
        public void getByNombreRTest()
        {
            string nombreR = "Jose Luis";
            List<PuntosRecogidaVM> zz = (List<PuntosRecogidaVM>)prepo.getByNombreR(nombreR);
        }

        [TestMethod]
        public void getByParametrosTest()
        {
            //Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //dictionary.Add("CODIGOP", "C9");
            //dictionary.Add("NOMBRE", "AFRICOR BRETOÑA");
            //dictionary.Add("RECOGEDOR", "Jose Luis");

            //List <PuntosRecogidaVM> zz = (List <PuntosRecogidaVM>) prepo.getByParametro(dictionary);
        }

        [TestMethod]
        public void insertTest()
        {
            PuntosRecogidaVM pr = prepo.getById(157);
            PuntosRecogidaVM pra = new PuntosRecogidaVM();

            pra.puntosRecogida.IDUSUARIO = pr.puntosRecogida.IDUSUARIO;
            pra.puntosRecogida.NOMBRE = pr.puntosRecogida.NOMBRE;
            pra.puntosRecogida.POSX = pr.puntosRecogida.POSX;
            pra.puntosRecogida.POSY = pr.puntosRecogida.POSY;
            pra.puntosRecogida.CODIGOP = "LO42";
            pra.puntosRecogida.COORDX = pr.puntosRecogida.COORDX;
            pra.puntosRecogida.COORDY = pr.puntosRecogida.COORDY;
            pra.puntosRecogida.DESCRIPCION = pr.puntosRecogida.DESCRIPCION;
            pra.puntosRecogida.DIRGPS = pr.puntosRecogida.DIRGPS;
            pra.puntosRecogida.DISTANCIA = pr.puntosRecogida.DISTANCIA;

            prepo.Insert(pra);
            prepo.Save();
        }

        [TestMethod]
        public void updateTest()
        {
            PuntosRecogidaVM pr = prepo.getById(160);
            pr.puntosRecogida.CODIGOP = "XXXX";
            prepo.Update(pr);
            prepo.Save();
        }

        [TestMethod]
        public void deleteTest()
        {
            PuntosRecogidaVM pr = prepo.getById(160);
            prepo.Delete(pr);
            prepo.Save();
        }


        [TestMethod]
        public void insertNoRepoTest()
        {
            PuntosRecogidaVM pr = prepo.getById(157);
            PuntosRecogidaVM pra = new PuntosRecogidaVM();

            pra.puntosRecogida.IDUSUARIO = pr.puntosRecogida.IDUSUARIO;
            pra.puntosRecogida.NOMBRE = pr.puntosRecogida.NOMBRE;
            pra.puntosRecogida.POSX = pr.puntosRecogida.POSX;
            pra.puntosRecogida.POSY = pr.puntosRecogida.POSY;
            pra.puntosRecogida.CODIGOP = "ZZZ";
            pra.puntosRecogida.COORDX = pr.puntosRecogida.COORDX;
            pra.puntosRecogida.COORDY = pr.puntosRecogida.COORDY;
            pra.puntosRecogida.DESCRIPCION = pr.puntosRecogida.DESCRIPCION;
            pra.puntosRecogida.DIRGPS = pr.puntosRecogida.DIRGPS;
            pra.puntosRecogida.DISTANCIA = pr.puntosRecogida.DISTANCIA;
            pra.puntosRecogida.IDUSUARIOR = pr.puntosRecogida.IDUSUARIOR;

            pra.puntosRecogida.ROWID = System.Guid.NewGuid().ToString();

            context.GEN_PUNTOSRECOGIDA.Add(pra.puntosRecogida);
            context.SaveChanges();
        }


        [TestMethod]
        public void updateNoRepoTest()
        {
            PuntosRecogidaVM pr = prepo.getById(160);
            pr.puntosRecogida.CODIGOP = "MANOLO";
            context.Entry(pr.puntosRecogida).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

    }
}
