using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using Tekla.Structures;
using Tekla.Structures.Model;
using TSMO=Tekla.Structures.Model.Operations;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Model.Operations;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Filtering;
using Tekla.Structures.Filtering.Categories;




using System.Collections;



namespace DSTVtoFolders
{




    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {

            #region connection to Tekla Structures and basic model path, version information

            Model Model1 = new Model();
            string modelname = Model1.GetInfo().ModelName;
            string modelpath = Model1.GetInfo().ModelPath;
            string configuration = ModuleManager.Configuration.ToString();
            string TSversion = TeklaStructuresInfo.GetCurrentProgramVersion();

            string tB = textBox1.Text;
            char ch = '*';

            List<Beam> ListBeamPlate = new List<Beam>(); // List Beam
            List<ContourPlate> ListPlate = new List<ContourPlate>(); //List Plate
            List<PolyBeam> ListPolyBeam = new List<PolyBeam>(); //List Poly Beam

            ArrayList AllParts = new ArrayList();





            #endregion

            string CNC_Files = textBox2.Text;

            if (!Directory.Exists(modelpath + @"\" + CNC_Files))
            {
                Directory.CreateDirectory(modelpath + @"\" + CNC_Files);
            }

            //Выбор деталей 

            Picker Picker = new Picker();

            try
            {


                //Перечесляем выбранные детали
                ModelObjectEnumerator input = Picker.PickObjects(Picker.PickObjectsEnum.PICK_N_PARTS, "Выберите детали");

                IEnumerator enumerator = input.GetEnumerator();


                //добовляем в eрау лист
                while (enumerator.MoveNext())
                {
                    Beam beam = enumerator.Current as Beam;
                    ContourPlate contourPlate = enumerator.Current as ContourPlate;
                    PolyBeam polyBeam = enumerator.Current as PolyBeam;

                    if (beam != null)
                    {
                        ListBeamPlate.Add(beam);
                    }
                    if (contourPlate != null)
                    {
                        ListPlate.Add(contourPlate);
                    }
                    if (polyBeam != null)
                    {
                        ListPolyBeam.Add(polyBeam);
                    }


                }



                #region
                //    ArrayList ObjectsToSelect = new ArrayList();

                //    Part part = enumerator.Current as Part;
                //    ObjectsToSelect.Add(part);                    

                // string partProfileF = part.Profile.ProfileString;
                //    char ch = '*';
                //int partProfileF2 = partProfileF.IndexOf(ch);                

                //ArrayList pathPartA = new ArrayList();                             

                //try { string partProfileF3 = partProfileF.Remove(partProfileF.IndexOf('*'), partProfileF.Length - partProfileF.IndexOf('*')); pathPartA.Add(partProfileF3); }
                //catch { string partProfileF3 = partProfileF; pathPartA.Add(partProfileF3); }

                //    string partProfileF1 = pathPartA[0].ToString();

                //    int indexOf = partProfileF.IndexOf("PL");
                //    string partMaterial = part.Material.MaterialString;
                //    Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
                //    MS.Select(ObjectsToSelect);


                //    if (indexOf == 0)
                //    {

                //        if (!Directory.Exists(modelpath + @"\" + CNC_Files + @"\" + partProfileF1 + "_" + partMaterial))
                //        {
                //            Directory.CreateDirectory(modelpath + @"\" + CNC_Files + @"\" + partProfileF1 + "_" + partMaterial);
                //        }


                //        string pathPart = modelpath + @"\" + CNC_Files + @"\" + partProfileF1 + "_" + partMaterial + @"\";


                //        Operation.CreateNCFilesFromSelected("DSTV for plates", pathPart);

                //        Operation.DisplayPrompt("ok");
                //    }

                // }
                #endregion
            }
            catch { Operation.DisplayPrompt("что то пошло не так..."); }



            // находим в пластины замоделенной балке
            int countPlateBeamT = 0;

            List<MyPlate> PlateBeamT = new List<MyPlate>();
            //List<MyPlate> PlatePBeamT = new List<MyPlate>();

            foreach (Beam beam in ListBeamPlate)
            {
                string PB = beam.Profile.ProfileString;

                if (PB.StartsWith(tB))
                {

                    string PP1 = PB.Substring(0, PB.LastIndexOf(ch));
                    string PP2 = beam.Material.MaterialString;
                    MyPlate myPlate = new MyPlate
                    {
                        PlateProfile = PP1,
                        PlateMaterial = PP2
                    };
                    PlateBeamT.Add(myPlate);

                    countPlateBeamT++;
                }
            }

            foreach (PolyBeam beam in ListPolyBeam)
            {
                string PB = beam.Profile.ProfileString;

                if (PB.StartsWith(tB))
                {

                    string PP1 = PB.Substring(0, PB.LastIndexOf(ch));
                    string PP2 = beam.Material.MaterialString;
                    MyPlate myPlate = new MyPlate
                    {
                        PlateProfile = PP1,
                        PlateMaterial = PP2
                    };
                    PlateBeamT.Add(myPlate);

                    countPlateBeamT++;
                }
            }



            //Убираем все что полсле *
            //ArrayList PlateBeamT = new ArrayList();

            //--------------------
            //foreach (Beam plateBeam in arrayList)
            //{
            //    string PB = plateBeam.Profile.ProfileString;  

            //    string[] plateT = { PB.Substring(0, PB.LastIndexOf(ch)), plateBeam.Material.MaterialString };

            //    PlateBeamT.Add(plateT);                               
            //}

            // -------------------------
            int countPlatePlate = 0;

            // List<MyPlate> PlatePlate = new List<MyPlate>();
            // List<MyPlate> PlatePlate = new List<MyPlate>();

            //ArrayList _myPlate = new ArrayList();

            //MyPlate myPlate = new MyPlate();
            // _myPlate = myPlate; 

            foreach (ContourPlate contourPlate in ListPlate)
            {
                //string[] PP = { contourPlate.Profile.ProfileString, contourPlate.Material.MaterialString };
                string PP1 = contourPlate.Profile.ProfileString;
                string PP2 = contourPlate.Material.MaterialString;
                // string[] _PP1_2 = { PP1, PP2 };
                MyPlate myPlate = new MyPlate
                {
                    PlateProfile = PP1,
                    PlateMaterial = PP2
                };


                //PlatePlate.AddRange(myPlate);
                PlateBeamT.Add(myPlate);
                countPlatePlate++;
            }


            //Объединение list<>
            //List<MyPlate> ListPlatesFilter = new List<MyPlate>();


            // private static void addListPlate (List<MyPlate> ListPlatesFilter, MyPlate mp, )

            List<MyPlate> filterMP = new List<MyPlate>();
            List<MyPlate> filterMP2 = new List<MyPlate>();
            List<MyPlate> filterMP2Sorted = new List<MyPlate>(); // итоговый список



            var dist = PlateBeamT.Distinct(new MyPlateComparer());

            





            foreach (MyPlate s in dist)
            {

                try
                {
                    string PP = s.PlateProfile.Substring(0, s.PlateProfile.LastIndexOf("."));
                    string PM = s.PlateMaterial;
                    MyPlate myPlate = new MyPlate
                    {
                        PlateProfile = PP,
                        PlateMaterial = PM,
                    };
                    filterMP.Add(myPlate);

                }
                catch
                {

                    string PP = s.PlateProfile;
                    string PM = s.PlateMaterial;
                    MyPlate myPlate = new MyPlate
                    {
                        PlateProfile = PP,
                        PlateMaterial = PM,
                    };
                    filterMP.Add(myPlate);

                }



            }


            var dist2 = filterMP.Distinct(new MyPlateComparer());

            foreach (MyPlate d in dist2)
            {
                filterMP2.Add(d);
            }

            var SortedfMP2 = from u in filterMP2
                             orderby u.PlateMaterial, u.PlateProfile
                             select u;


            foreach (MyPlate u in SortedfMP2)
            {
                filterMP2Sorted.Add(u);
            }

            //выбор деталей по толщине и марке стали

            for (int i = 0; i < filterMP2Sorted.Count; i++)
            {

                try
                {
                    //List<ContourPlate> y1 = new List<ContourPlate>();
                    //List<Beam> y2 = new List<Beam>();
                    ArrayList y1 = new ArrayList();

                    var x1 = from plate in ListPlate
                             where plate.Profile.ProfileString == filterMP2Sorted[i].PlateProfile
                             where plate.Material.MaterialString == filterMP2Sorted[i].PlateMaterial
                             select plate;

                    var x2 = from plateB in ListBeamPlate
                             where plateB.Profile.ProfileString.StartsWith(filterMP2Sorted[i].PlateProfile)
                             where plateB.Material.MaterialString == filterMP2Sorted[i].PlateMaterial
                             select plateB;

                    var x3 = from plateB in ListPolyBeam
                             where plateB.Profile.ProfileString.StartsWith(filterMP2Sorted[i].PlateProfile)
                             where plateB.Material.MaterialString == filterMP2Sorted[i].PlateMaterial
                             select plateB;



                    foreach (ContourPlate plate in x1)
                    { y1.Add(plate); }

                    foreach (Beam beam in x2)
                    { y1.Add(beam); }

                    foreach (PolyBeam polyBeam in x3)
                    { y1.Add(polyBeam); }


                    Tekla.Structures.Model.UI.ModelObjectSelector MS = new Tekla.Structures.Model.UI.ModelObjectSelector();
                    MS.Select(y1);
                    // MS.Select(y1);

                    if (!Directory.Exists(modelpath + @"\" + CNC_Files + @"\" + filterMP2Sorted[i].PlateProfile + "_" + filterMP2Sorted[i].PlateMaterial))
                    {
                        Directory.CreateDirectory(modelpath + @"\" + CNC_Files + @"\" + filterMP2Sorted[i].PlateProfile + "_" + filterMP2Sorted[i].PlateMaterial);
                    }


                    string pathPart = modelpath + @"\" + CNC_Files + @"\" + filterMP2Sorted[i].PlateProfile + "_" + filterMP2Sorted[i].PlateMaterial + @"\";


                    Operation.CreateNCFilesFromSelected("DSTV for plates", pathPart);

                    // Operation.DisplayPrompt("ok");



                }

                catch { Operation.DisplayPrompt("что то пошло не так...."); }



            }

            Tekla.Structures.Model.UI.ModelObjectSelector MS1 = new Tekla.Structures.Model.UI.ModelObjectSelector();
            MS1.Select(AllParts);
            Operation.DisplayPrompt("Готово... //work by Wiz//");


        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
