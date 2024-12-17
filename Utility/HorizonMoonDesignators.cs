using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace OurSolarSystemAPI.Utility
{
    public struct HorizonMoonDesignators(int id, string name, string designation)
    {
        public int ID = id;
        public string Name = name;
        public string Designation = designation;
    }


    public class MoonContainer(string name, List<HorizonMoonDesignators> moons, int planetHorizonId, int barycenterHorizonId)
    {
        public string Name = name;
        public int PlanetHorizonId = planetHorizonId;
        public List<HorizonMoonDesignators> Moons = moons;
        public int BarycenterHorizonId = barycenterHorizonId;


        public static List<MoonContainer> InstantiatePlanetAndMoonStructs() 
        {
            var earthMoons = new List<HorizonMoonDesignators>
            {
                new HorizonMoonDesignators(301, "Moon", "Luna")
            };

            var marsMoons = new List<HorizonMoonDesignators>
            {
                new HorizonMoonDesignators(401, "Phobos", "MI"),
                new HorizonMoonDesignators(402, "Deimos", "MII")
            };

            var jupiterMoons = new List<HorizonMoonDesignators>
            {
                new HorizonMoonDesignators(501, "Io", "JI"),
                new HorizonMoonDesignators(502, "Europa", "JII"),
                new HorizonMoonDesignators(503, "Ganymede", "JIII"),
                new HorizonMoonDesignators(504, "Callisto", "JIV"),
                new HorizonMoonDesignators(505, "Amalthea", "JV"),
                new HorizonMoonDesignators(506, "Himalia", "JVI"),
                new HorizonMoonDesignators(507, "Elara", "JVII"),
                new HorizonMoonDesignators(508, "Pasiphae", "JVIII"),
                new HorizonMoonDesignators(509, "Sinope", "JIX"),
                new HorizonMoonDesignators(510, "Lysithea", "JX"),
                new HorizonMoonDesignators(511, "Carme", "JXI"),
                new HorizonMoonDesignators(512, "Ananke", "JXII"),
                new HorizonMoonDesignators(513, "Leda", "JXIII"),
                new HorizonMoonDesignators(514, "Thebe", "JXIV"),
                new HorizonMoonDesignators(515, "Adrastea", "JXV"),
                new HorizonMoonDesignators(516, "Metis", "JXVI"),
                new HorizonMoonDesignators(517, "Callirrhoe", "JXVII"),
                new HorizonMoonDesignators(518, "Themisto", "JXVIII"),
                new HorizonMoonDesignators(519, "Megaclite", "JXIX"),
                new HorizonMoonDesignators(520, "Taygete", "JXX"),
                new HorizonMoonDesignators(521, "Chaldene", "JXXI"),
                new HorizonMoonDesignators(522, "Harpalyke", "JXXII"),
                new HorizonMoonDesignators(523, "Kalyke", "JXXIII"),
                new HorizonMoonDesignators(524, "Iocaste", "JXXIV"),
                new HorizonMoonDesignators(525, "Erinome", "JXXV"),
                new HorizonMoonDesignators(526, "Isonoe", "JXXVI"),
                new HorizonMoonDesignators(527, "Praxidike", "JXXVII"),
                new HorizonMoonDesignators(528, "Autonoe", "JXXVIII"),
                new HorizonMoonDesignators(529, "Thyone", "JXXIX"),
                new HorizonMoonDesignators(530, "Hermippe", "JXXX"),
                new HorizonMoonDesignators(531, "Aitne", "JXXXI"),
                new HorizonMoonDesignators(532, "Eurydome", "JXXXII"),
                new HorizonMoonDesignators(533, "Euanthe", "JXXXIII"),
                new HorizonMoonDesignators(534, "Euporie", "JXXXIV"),
                new HorizonMoonDesignators(535, "Orthosie", "JXXXV"),
                new HorizonMoonDesignators(536, "Sponde", "JXXXVI"),
                new HorizonMoonDesignators(537, "Kale", "JXXXVII"),
                new HorizonMoonDesignators(538, "Pasithee", "JXXXVIII"),
                new HorizonMoonDesignators(539, "Hegemone", "JXXXIX"),
                new HorizonMoonDesignators(540, "Mneme", "JXL"),
                new HorizonMoonDesignators(541, "Aoede", "JXLI"),
                new HorizonMoonDesignators(542, "Thelxinoe", "JXLII"),
                new HorizonMoonDesignators(543, "Arche", "JXLIII"),
                new HorizonMoonDesignators(544, "Kallichore", "JXLIV"),
                new HorizonMoonDesignators(545, "Helike", "JXLV"),
                new HorizonMoonDesignators(546, "Carpo", "JXLVI"),
                new HorizonMoonDesignators(547, "Eukelade", "JXLVII"),
                new HorizonMoonDesignators(548, "Cyllene", "JXLVIII"),
                new HorizonMoonDesignators(549, "Kore", "JXLIX"),
                new HorizonMoonDesignators(550, "Herse", "JXLXII"),
                new HorizonMoonDesignators(551, "(Unnamed 2010J1)", "JLI"),
                new HorizonMoonDesignators(552, "(Unnamed 2010J2)", "JLII"),
                new HorizonMoonDesignators(553, "Dia", "JLIII"),
                new HorizonMoonDesignators(554, "(Unnamed 2016J1)", "JLIV"),
                new HorizonMoonDesignators(555, "(Unnamed 2003J18)", "JLV"),
                new HorizonMoonDesignators(556, "(Unnamed 2011J2)", "JLVI"),
                new HorizonMoonDesignators(557, "Eirene", "JLVII"),
                new HorizonMoonDesignators(558, "Philophrosyne", "JLVIII"),
                new HorizonMoonDesignators(559, "(Unnamed 2017J1)", "JLIX"),
                new HorizonMoonDesignators(560, "Eupheme", "JLX"),
                new HorizonMoonDesignators(561, "(Unnamed 2003J19)", ""),
                new HorizonMoonDesignators(562, "Valetudo", ""),
                new HorizonMoonDesignators(563, "(Unnamed 2017J2)", ""),
                new HorizonMoonDesignators(564, "(Unnamed 2017J3)", ""),
                new HorizonMoonDesignators(565, "Pandia", "JLXV"),
                new HorizonMoonDesignators(566, "(Unnamed 2017J5)", ""),
                new HorizonMoonDesignators(567, "(Unnamed 2017J6)", ""),
                new HorizonMoonDesignators(568, "(Unnamed 2017J7)", ""),
                new HorizonMoonDesignators(569, "(Unnamed 2017J8)", ""),
                new HorizonMoonDesignators(570, "(Unnamed 2017J9)", "JLXX"),
                new HorizonMoonDesignators(571, "Ersa", ""),
                new HorizonMoonDesignators(572, "(Unnamed 2011J1)", "")
            };

            var saturnMoons = new List<HorizonMoonDesignators> 
            {
                new HorizonMoonDesignators(601, "Mimas", "SI"),
                new HorizonMoonDesignators(602, "Enceladus", "SII"),
                new HorizonMoonDesignators(603, "Tethys", "SIII"),
                new HorizonMoonDesignators(604, "Dione", "SIV"),
                new HorizonMoonDesignators(605, "Rhea", "SV"),
                new HorizonMoonDesignators(606, "Titan", "SVI"),
                new HorizonMoonDesignators(607, "Hyperion", "SVII"),
                new HorizonMoonDesignators(608, "Iapetus", "SVIII"),
                new HorizonMoonDesignators(609, "Phoebe", "SIX"),
                new HorizonMoonDesignators(610, "Janus", "SX"),
                new HorizonMoonDesignators(611, "Epimetheus", "SXI"),
                new HorizonMoonDesignators(612, "Helene", "SXII"),
                new HorizonMoonDesignators(613, "Telesto", "SXIII"),
                new HorizonMoonDesignators(614, "Calypso", "SXIV"),
                new HorizonMoonDesignators(615, "Atlas", "SXV"),
                new HorizonMoonDesignators(616, "Prometheus", "SXVI"),
                new HorizonMoonDesignators(617, "Pandora", "SXVII"),
                new HorizonMoonDesignators(618, "Pan", "SXVIII"),
                new HorizonMoonDesignators(619, "Ymir", "SXIX"),
                new HorizonMoonDesignators(620, "Paaliaq", "SXX"),
                new HorizonMoonDesignators(621, "Tarvos", "SXXI"),
                new HorizonMoonDesignators(622, "Ijiraq", "SXXII"),
                new HorizonMoonDesignators(623, "Suttungr", "SXXIII"),
                new HorizonMoonDesignators(624, "Kiviuq", "SXXIV"),
                new HorizonMoonDesignators(625, "Mundilfari", "SXXV"),
                new HorizonMoonDesignators(626, "Albiorix", "SXXVI"),
                new HorizonMoonDesignators(627, "Skathi", "SXXVII"),
                new HorizonMoonDesignators(628, "Erriapus", "SXXVIII"),
                new HorizonMoonDesignators(629, "Siarnaq", "SXXIX"),
                new HorizonMoonDesignators(630, "Thrymr", "SXXX"),
                new HorizonMoonDesignators(631, "Narvi", "SXXXI"),
                new HorizonMoonDesignators(632, "Methone", "SXXXII"),
                new HorizonMoonDesignators(633, "Pallene", "SXXXIII"),
                new HorizonMoonDesignators(634, "Polydeuces", "SXXXIV"),
                new HorizonMoonDesignators(635, "Daphnis", "SXXXV"),
                new HorizonMoonDesignators(636, "Aegir", "SXXXVI"),
                new HorizonMoonDesignators(637, "Bebhionn", "SXXXVII"),
                new HorizonMoonDesignators(638, "Bergelmir", "SXXXVIII"),
                new HorizonMoonDesignators(639, "Bestla", "SXXXIX"),
                new HorizonMoonDesignators(640, "Farbauti", "SXL"),
                new HorizonMoonDesignators(641, "Fenrir", "SXLII"),
                new HorizonMoonDesignators(642, "Fornjot", "SXLIII"),
                new HorizonMoonDesignators(643, "Hati", "SXLV"),
                new HorizonMoonDesignators(644, "Unnamed 2004S26", "SXLVI"),
                new HorizonMoonDesignators(645, "Kari", "SXLVII"),
                new HorizonMoonDesignators(646, "Unnamed 2007S3", "SXLVIII"),
                new HorizonMoonDesignators(647, "Loge", "SXLIX"),
                new HorizonMoonDesignators(648, "Skoll", "SXLX"),
                new HorizonMoonDesignators(649, "Surtur", "SXLXI"),
                new HorizonMoonDesignators(650, "Anthe", "SXLXII"),
                new HorizonMoonDesignators(651, "Unnamed 2004S20", "SXLXIII"),
                new HorizonMoonDesignators(652, "Unnamed 2006S1", "SXLXIV"),
                new HorizonMoonDesignators(653, "Unnamed 2004S23", "SXLXV") 
            };

            var uranusMoons = new List<HorizonMoonDesignators> 
            {
                new HorizonMoonDesignators(701, "Miranda", "UI"),
                new HorizonMoonDesignators(702, "Ariel", "UII"),
                new HorizonMoonDesignators(703, "Umbriel", "UIII"),
                new HorizonMoonDesignators(704, "Titania", "UIV"),
                new HorizonMoonDesignators(705, "Oberon", "UV"),
                new HorizonMoonDesignators(706, "Caliban", "UVI"),
                new HorizonMoonDesignators(707, "Sycorax", "UVII"),
                new HorizonMoonDesignators(708, "Prospero", "UVIII"),
                new HorizonMoonDesignators(709, "Setebos", "UIX"),
                new HorizonMoonDesignators(710, "Stephano", "UX"),
                new HorizonMoonDesignators(711, "Trinculo", "UXI"),
                new HorizonMoonDesignators(712, "Francisco", "UXII"),
                new HorizonMoonDesignators(713, "Margaret", "UXIII"),
                new HorizonMoonDesignators(714, "Ferdinand", "UXIV"),
                new HorizonMoonDesignators(715, "Perdita", "UXV"),
                new HorizonMoonDesignators(716, "Mab", "UXVI"),
                new HorizonMoonDesignators(717, "Cupid", "UXVII")
            };

            var neptuneMoons = new List<HorizonMoonDesignators> 
            {
                new HorizonMoonDesignators(801, "Triton", "NI"),
                new HorizonMoonDesignators(802, "Nereid", "NII"),
                new HorizonMoonDesignators(803, "Naiad", "NIII"),
                new HorizonMoonDesignators(804, "Thalassa", "NIV"),
                new HorizonMoonDesignators(805, "Despina", "NV"),
                new HorizonMoonDesignators(806, "Galatea", "NVI"),
                new HorizonMoonDesignators(807, "Larissa", "NVII"),
                new HorizonMoonDesignators(808, "Proteus", "NVIII"),
                new HorizonMoonDesignators(809, "Halimede", "NIX"),
                new HorizonMoonDesignators(810, "Psamathe", "NX"),
                new HorizonMoonDesignators(811, "Sao", "NXI"),
                new HorizonMoonDesignators(812, "Laomedeia", "NXII"),
                new HorizonMoonDesignators(813, "Neso", "NXIII")
            };

            return new List<MoonContainer>
            {
                new MoonContainer("earth", earthMoons, 399, 3),
                new MoonContainer("mars", marsMoons, 499, 4),
                new MoonContainer("jupiter", jupiterMoons, 599, 5),
                new MoonContainer("saturn", saturnMoons, 699, 6),
                new MoonContainer("uranus", uranusMoons,799, 7),
                new MoonContainer("neptune", neptuneMoons, 899, 8)
            };                   
        }
    }
}
