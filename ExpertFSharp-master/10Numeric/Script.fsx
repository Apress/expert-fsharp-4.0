#load "packages/FSharp.Charting/FSharp.Charting.fsx"

open FSharp.Charting

let rnd = System.Random()
let rand() = rnd.NextDouble()

let randomPoints = [for i in 0 .. 1000 -> 10.0 * rand(), 10.0 * rand()]

randomPoints |> Chart.Point

//[Loading .\10Numeric\packages\FSharp.Charting\FSharp.Charting.fsx]
//
//namespace FSI_0002.FSharp
//  module FsiAutoShow = begin
//  end
//
//
//val rnd : System.Random
//val rand : unit -> float
//val randomPoints : (float * float) list =
//  [(6.719770556, 8.759320517); (0.4070158957, 7.947359596);
//   (1.27230269, 9.982769717); (6.605013198, 9.635140397);
//   (9.97514898, 1.546832743); (0.8711712672, 9.096940206);
//   (9.912862689, 9.585225419); (5.24524898, 0.00821496826);
//   (5.236180376, 5.775958163); (1.419311232, 8.62085151);
//   (3.982047147, 7.087851626); (0.2350487282, 6.44357879);
//   (3.375123326, 3.297089796); (0.5214014, 0.2403858864);
//   (0.0445700763, 9.373674714); (2.868117608, 9.653944317);
//   (0.1067469735, 2.285530447); (3.320962658, 4.322286683);
//   (3.772653706, 2.825243926); (5.789691315, 2.658191413);
//   (2.344007661, 3.243329061); (1.906595441, 1.110062409);
//   (9.733880502, 8.40739496); (1.828760501, 6.013931691);
//   (7.827114355, 6.710959508); (4.753920517, 7.846295115);
//   (3.278024766, 5.704100158); (2.520160509, 9.631918929);
//   (8.524271789, 3.963437106); (4.57223627, 7.975212893);
//   (9.461368317, 6.364627311); (9.590570321, 0.6014742658);
//   (8.678715135, 1.21722695); (8.990193232, 7.627332242);
//   (6.264262761, 0.9229622972); (6.235561262, 2.41093645);
//   (9.986266848, 8.761119819); (6.276843849, 0.7387180863);
//   (5.181256186, 9.124986319); (6.709698288, 4.967728367);
//   (1.468329295, 4.507469709); (2.413271532, 3.333610568);
//   (4.619754196, 5.021822492); (6.375919551, 4.402646816);
//   (9.765369939, 3.689043728); (5.798014894, 9.809216601);
//   (8.253007656, 7.814478421); (3.196823096, 5.97938035);
//   (3.65275874, 1.305121175); (2.431347273, 8.516653552);
//   (9.417201727, 4.201428259); (9.74966893, 6.904152058);
//   (0.4753982464, 2.342984067); (7.860028268, 4.516904948);
//   (9.427256309, 1.781442422); (4.450662744, 9.39928547);
//   (7.253738817, 9.604507903); (6.506883598, 4.953898608);
//   (3.95135578, 6.256959753); (5.981720069, 3.656892643);
//   (4.841307399, 4.587546417); (7.861962303, 2.575219033);
//   (5.124947403, 6.426344661); (4.157928794, 2.171788426);
//   (5.564296723, 0.2974634991); (7.085959347, 3.876135011);
//   (6.693639046, 8.193044736); (5.550526639, 7.266901036);
//   (4.75780078, 5.509119474); (2.858212321, 2.276770129);
//   (7.161794224, 1.859014603); (4.975390506, 7.983927516);
//   (9.238380985, 6.398729424); (2.555477783, 8.648499753);
//   (1.307594823, 8.242924487); (2.02802457, 7.395798986);
//   (5.323401105, 8.77445463); (3.675346153, 4.829655311);
//   (6.339465955, 7.174449897); (1.779204654, 4.049053585);
//   (8.185055274, 5.688239842); (8.952608224, 9.12979281);
//   (4.695483076, 0.5745277324); (2.705646424, 9.060694081);
//   (4.053981264, 9.239982562); (0.1960978285, 8.442236305);
//   (3.398747432, 3.70494994); (6.495098419, 2.982292796);
//   (9.61215591, 9.878034787); (3.336838048, 8.726217979);
//   (3.870866878, 5.509429041); (0.8641936029, 7.321372236);
//   (8.269438929, 9.69016036); (8.552733906, 7.919184416);
//   (4.517698583, 0.7208713287); (0.927435081, 7.583350883);
//   (3.72991482, 8.809158736); (4.091714855, 1.473554383);
//   (2.906406379, 5.845597696); (3.28844444, 8.663853253); ...]
//val it : FSharp.Charting.ChartTypes.GenericChart = (Chart)

let randomTrend1 = [for i in 0.0 .. 0.1 .. 10.0 -> i, sin i + rand()]
let randomTrend2 = [for i in 0.0 .. 0.1 .. 10.0 -> i, sin i + rand()]

Chart.Combine [Chart.Line randomTrend1; Chart.Point randomTrend2]

//val randomTrend1 : (float * float) list =
//  [(0.0, 0.44980574); (0.1, 0.8834455957); (0.2, 1.159471579);
//   (0.3, 0.7178485123); (0.4, 0.5504947214); (0.5, 1.234162973);
//   (0.6, 1.087795148); (0.7, 0.8366222267); (0.8, 1.104606323);
//   (0.9, 1.656457652); (1.0, 0.927734451); (1.1, 1.442332348);
//   (1.2, 1.10660359); (1.3, 1.552970731); (1.4, 1.764796001);
//   (1.5, 1.110436868); (1.6, 1.957842482); (1.7, 1.001929661);
//   (1.8, 1.044275724); (1.9, 1.631750392); (2.0, 1.415156412);
//   (2.1, 1.636701237); (2.2, 1.589149224); (2.3, 0.9470990196);
//   (2.4, 0.8215375213); (2.5, 1.304234222); (2.6, 1.086488836);
//   (2.7, 0.8996540791); (2.8, 1.059507448); (2.9, 0.5421462835);
//   (3.0, 0.3264374784); (3.1, 0.403969253); (3.2, 0.9413837867);
//   (3.3, 0.3706832462); (3.4, 0.05488202968); (3.5, 0.1778179203);
//   (3.6, -0.3318296055); (3.7, 0.2857004293); (3.8, 0.05745918954);
//   (3.9, -0.08274383326); (4.0, -0.4518629877); (4.1, -0.1767406938);
//   (4.2, -0.4683677572); (4.3, -0.9092019731); (4.4, -0.4000834276);
//   (4.5, -0.9353930257); (4.6, -0.7344794302); (4.7, -0.4891334397);
//   (4.8, -0.2971160414); (4.9, -0.3921326962); (5.0, -0.9151117422);
//   (5.1, 0.02586913089); (5.2, 0.1115602232); (5.3, -0.1806106499);
//   (5.4, -0.1205452323); (5.5, -0.0292264556); (5.6, -0.6283072789);
//   (5.7, 0.2087228977); (5.8, -0.1883482145); (5.9, 0.08143763587);
//   (6.0, -0.09566552806); (6.1, -0.1312840288); (6.2, 0.3847958392);
//   (6.3, 0.101167178); (6.4, 0.8043624774); (6.5, 0.9389948638);
//   (6.6, 0.8629084217); (6.7, 1.050985484); (6.8, 0.773102765);
//   (6.9, 0.8291848878); (7.0, 0.6592376426); (7.1, 0.8717013494);
//   (7.2, 1.134615634); (7.3, 1.315842388); (7.4, 1.279218892);
//   (7.5, 1.802322545); (7.6, 1.338203527); (7.7, 1.76185709);
//   (7.8, 1.648418506); (7.9, 1.102878591); (8.0, 1.435908752);
//   (8.1, 1.030087458); (8.2, 1.713956188); (8.3, 1.036371215);
//   (8.4, 1.11368333); (8.5, 1.03212077); (8.6, 1.101770809);
//   (8.7, 1.011070368); (8.8, 1.461126878); (8.9, 1.135130118);
//   (9.0, 0.9377602742); (9.1, 0.6703807598); (9.2, 0.7621725194);
//   (9.3, 0.3384572033); (9.4, 0.4460477812); (9.5, 0.1789099117);
//   (9.6, -0.0006756060343); (9.7, 0.0470941113); (9.8, -0.04732843818);
//   (9.9, 0.3701078769); ...]
//val randomTrend2 : (float * float) list =
//  [(0.0, 0.6130760101); (0.1, 0.3316338207); (0.2, 0.6469727748);
//   (0.3, 0.8835890792); (0.4, 1.290498566); (0.5, 1.090161582);
//   (0.6, 1.094251585); (0.7, 0.9153636831); (0.8, 1.505252778);
//   (0.9, 1.089356925); (1.0, 1.070741487); (1.1, 1.000740639);
//   (1.2, 1.104355802); (1.3, 0.9723219811); (1.4, 1.109002053);
//   (1.5, 1.275147831); (1.6, 1.333259464); (1.7, 1.816933666);
//   (1.8, 1.428027246); (1.9, 1.302801252); (2.0, 1.112563347);
//   (2.1, 1.633135245); (2.2, 1.453376556); (2.3, 1.470808547);
//   (2.4, 1.326431827); (2.5, 1.201921848); (2.6, 0.6424463618);
//   (2.7, 0.471513292); (2.8, 0.4614379145); (2.9, 0.9299207218);
//   (3.0, 0.1925491252); (3.1, 0.4961188278); (3.2, 0.763857247);
//   (3.3, 0.4554215208); (3.4, 0.5779333929); (3.5, 0.477614015);
//   (3.6, -0.1175982559); (3.7, 0.01629436753); (3.8, -0.2538536924);
//   (3.9, -0.06486854557); (4.0, 0.08096210491); (4.1, -0.7413219691);
//   (4.2, -0.7832627749); (4.3, -0.5880866901); (4.4, -0.6552307875);
//   (4.5, -0.7357809995); (4.6, -0.6267251145); (4.7, -0.7946842735);
//   (4.8, -0.6984445764); (4.9, -0.006044424404); (5.0, -0.1189589606);
//   (5.1, -0.4322288003); (5.2, -0.01848358011); (5.3, -0.3611248357);
//   (5.4, -0.4852603741); (5.5, 0.1376098062); (5.6, -0.04434638599);
//   (5.7, 0.1725145671); (5.8, 0.4724980466); (5.9, -0.07624614555);
//   (6.0, 0.2043755553); (6.1, 0.3033131954); (6.2, 0.06160682872);
//   (6.3, 0.1140391954); (6.4, 0.3711501029); (6.5, 0.9898523253);
//   (6.6, 0.5988432523); (6.7, 0.9639994218); (6.8, 0.669402652);
//   (6.9, 0.873594845); (7.0, 1.609717255); (7.1, 1.516524393);
//   (7.2, 1.260932521); (7.3, 1.681718622); (7.4, 1.41744466);
//   (7.5, 1.064310755); (7.6, 1.649532553); (7.7, 1.304969139);
//   (7.8, 1.427275393); (7.9, 1.40816087); (8.0, 1.225842061);
//   (8.1, 1.891595817); (8.2, 1.687143936); (8.3, 1.05221341);
//   (8.4, 1.705304987); (8.5, 1.356330348); (8.6, 1.323964188);
//   (8.7, 1.014058014); (8.8, 0.9105802944); (8.9, 1.49134522);
//   (9.0, 0.6535954761); (9.1, 0.9208204401); (9.2, 0.8319201965);
//   (9.3, 0.1848281028); (9.4, 0.1638819856); (9.5, 0.2771377801);
//   (9.6, 0.7579321292); (9.7, 0.7193270762); (9.8, -0.2930007807);
//   (9.9, 0.0641030554); ...]
//val it : ChartTypes.GenericChart = (Chart)

Chart.Line(randomPoints, Title = "Expected Trend")
//val it : ChartTypes.GenericChart = (Chart)

randomPoints
    |> fun c -> Chart.Line(c, Title = "Expected Trend")

true, false
//val it : bool * bool = (true, false)

0uy, 0x13uy, 19uy, 0xFFuy, 255uy
//val it : byte * byte * byte * byte * byte = (0uy, 19uy, 19uy, 255uy, 255uy)

0y, 0x13y, 19y, 0xFFy, -1y
//val it : sbyte * sbyte * sbyte * sbyte * sbyte = (0y, 19y, 19y, -1y, -1y)

0s, 19s, 0x0800s
//val it : int16 * int16 * int16 = (0s, 19s, 2048s)

0us, 19us, 0x0800us
//val it : uint16 * uint16 * uint16 = (0us, 19us, 2048us)

0, 19, 0x0800, 0b0001
//val it : int * int * int * int = (0, 19, 2048, 1)

0u, 19u, 0x0800u
//val it : uint32 * uint32 * uint32 = (0u, 19u, 2048u)

0L, 19L, 0x0800L
//val it : int64 * int64 * int64 = (0L, 19L, 2048L)

0UL, 19UL, 0x0800UL
//val it : uint64 * uint64 * uint64 = (0UL, 19UL, 2048UL)

0n, 19n, 0x0800n
//val it : nativeint * nativeint * nativeint = (0n, 19n, 2048n)

0un, 19un, 0x0800un
//val it : unativeint * unativeint * unativeint = (0un, 19un, 2048un)

0.0f, 19.7f, 1.3e4f
//val it : float32 * float32 * float32 = (0.0f, 19.7000008f, 13000.0f)

0.0, 19.7, 1.3e4
//val it : float * float * float = (0.0, 19.7, 13000.0)

0M, 19M, 19.03M
//val it : decimal * decimal * decimal = (0M, 19M, 19.03M)

0I, 19I
//val it : System.Numerics.BigInteger * System.Numerics.BigInteger =
//  (0 {IsEven = true;
//      IsOne = false;
//      IsPowerOfTwo = false;
//      IsZero = true;
//      Sign = 0;}, 19 {IsEven = false;
//                      IsOne = false;
//                      IsPowerOfTwo = false;
//                      IsZero = false;
//                      Sign = 1;})

System.Numerics.Complex(2.0, 3.0)
//val it : System.Numerics.Complex = (2, 3) {Imaginary = 3.0;
//                                           Magnitude = 3.605551275;
//                                           Phase = 0.9827937232;
//                                           Real = 2.0;}

2147483647 + 1
//val it : int = -2147483648

sbyte (-17)
//val it : sbyte = -17y

byte 255
//val it : byte = 255uy

int16 0
//val it : int16 = 0s

uint16 65535
//val it : uint16 = 65535us

int 17.8
//val it : int = 17

uint32 12
//val it : uint32 = 12u

int64 (-100.4)
//val it : int64 = -100L

uint64 1
//val it : uint64 = 1UL

decimal 65.3
//val it : decimal = 65.3M

float32 65
//val it : float32 = 65.0f

float 65
//val it : float = 65.0

nan = nan
//val it : bool = false

nan <= nan
//val it : bool = false

nan < nan
//val it : bool = false

abs -10.0f
//val it : float32 = 10.0f

cos 0.0
//val it : float = 1.0

cosh 1.0
//val it : float = 1.543080635

acos 1.0
//val it : float = 0.0

ceil 1.001
//val it : float = 2.0

truncate 8.9
//val it : float = 8.0

exp 1.0
//val it : float = 2.718281828

2.0 ** 4.0
//val it : float = 16.0

sprintf "0x%02X" (0x65 &&& 0x0F)
//val it : string = "0x05"

sprintf "0x%X" (0x65 ||| 0x18)
//val it : string = "0x7D"

sprintf "0x%X" (0x65 ^^^ 0x0F)
//val it : string = "0x6A"

sprintf "0x%X" (~~~0x65)
//val it : string = "0xFFFFFF9A"

sprintf "0x%02X" (0x01 <<< 3)
//val it : string = "0x08"

sprintf "0x%02X" (0x65 >>> 3)
//val it : string = "0x0C"

/// Encode an integer into 1, 2 or 5 bytes
let encode (n : int32) =
    if (n >= 0 && n <= 0x7F) then [n]
    elif (n >= 0x80 && n <= 0x3FFF) then
        [(0x80 ||| (n >>> 8)) &&& 0xFF;
         (n &&& 0xFF)]
    else  [0xC0;
           ((n >>> 24) &&& 0xFF);
           ((n >>> 16) &&& 0xFF);
           ((n >>> 8) &&& 0xFF);
           (n &&& 0xFF)]
//val encode : n:int32 -> int32 list

encode 32
//val it : int32 list = [32]

encode 320
//val it : int32 list = [129; 64]

encode 32000
//val it : int32 list = [192; 0; 0; 125; 0]

let rnd = new System.Random()
let rand() = rnd.NextDouble()
let data = [for i in 1 .. 1000 -> rand() * rand()]

let averageOfData = data |> Seq.average
let sumOfData = data |> Seq.sum
let maxOfData = data |> Seq.max
let minOfData = data |> Seq.min
//val rnd : System.Random
//val rand : unit -> float
//val data : float list =
//  [0.2208119354; 0.4042346511; 0.1435995269; 0.1535951804; 0.4819454534;
//   0.7136125263; 0.571467073; 0.4032733671; 0.4995212033; 0.1161086592;
//   0.119942718; 0.4689084621; 0.389977412; 0.2383124794; 0.08323103748;
//   0.07679674525; 0.1960887494; 0.205382659; 0.003664001599; 0.3799656213;
//   0.6228870957; 0.287268103; 0.01657984467; 0.09966630822; 0.2598533341;
//   0.4580685795; 0.06665523768; 0.6572553991; 0.5755644024; 0.03793924599;
//   0.62597875; 0.06874783245; 0.1223077499; 0.2133182195; 0.04153608306;
//   0.4691736419; 0.01521136459; 0.1177405735; 0.2673435035; 0.3500481638;
//   0.1277021912; 0.402880711; 0.04471365615; 0.1274664769; 0.4670568756;
//   0.1525458298; 0.7151503803; 0.03467968321; 0.2501995092; 0.002945209199;
//   0.4357574912; 0.2041458423; 0.654024584; 0.4460083783; 0.2873370477;
//   0.006493127807; 0.09467561509; 0.09974928564; 0.3584400035; 0.02884408097;
//   0.3298639066; 0.2864552375; 0.1727247553; 0.2759776376; 0.06216267332;
//   0.2080509339; 0.2489769696; 0.0308686288; 0.07015970076; 0.2648178584;
//   0.6822948043; 0.2625334521; 0.1750699732; 8.584933963e-05; 0.3659805124;
//   0.1360089776; 0.07148839634; 0.1099565576; 0.3102635885; 0.440546875;
//   0.2590987084; 0.5419329424; 0.4775015877; 0.04324073426; 0.3481475426;
//   0.129768269; 0.6507609208; 6.077197329e-05; 0.02581105385; 0.01859113737;
//   0.2143971152; 0.2437986654; 0.1570904108; 0.113042821; 0.8377263211;
//   0.01686618688; 0.04291055785; 0.2591415592; 0.08100868226; 0.5831289927;
//   ...]
//val averageOfData : float = 0.2502789312
//val sumOfData : float = 250.2789312
//val maxOfData : float = 0.9558598127
//val minOfData : float = 6.077197329e-05

type RandomPoint = {X : float; Y : float; Z : float}

let random3Dpoints = 
    [for i in 1 .. 1000 -> {X = rand(); Y = rand(); Z = rand()}]

let averageX = random3Dpoints |> Seq.averageBy (fun p -> p.X)
let averageY = random3Dpoints |> Seq.averageBy (fun p -> p.Y)
let averageZ = random3Dpoints |> Seq.averageBy (fun p -> p.Z)
//val averageX : float = 0.4983039237
//val averageY : float = 0.4912099001
//val averageZ : float = 0.5045033966

let maxY = random3Dpoints |> Seq.maxBy (fun p -> p.Y)
//val maxY : RandomPoint = {X = 0.235424088;
//                          Y = 0.99862719;
//                          Z = 0.5715418237;}

// See http://en.wikipedia.org/wiki/Euclidean_distance, for a discussion of norm and distance.
let norm (p : RandomPoint) = sqrt (p.X * p.X + p.Y * p.Y + p.Z * p.Z)
let closest = random3Dpoints |> Seq.minBy (fun p -> norm p)
//val norm : p:RandomPoint -> float
//val closest : RandomPoint = {X = 0.04515789638;
//                             Y = 0.07091009574;
//                             Z = 0.08101457687;}

let histogram = 
    random3Dpoints 
    |> Seq.countBy (fun p -> int (norm p * 10.0 / sqrt 3.0) ) 
    |> Seq.sortBy fst 
    |> Seq.toList

//val histogram : (int * int) list =
//  [(0, 3); (1, 14); (2, 49); (3, 119); (4, 157); (5, 245); (6, 221); (7, 142);
//   (8, 45); (9, 5)]

#I "./packages/FSharp.Core.Fluent-4.0/lib/portable-net45+netcore45+wpa81+wp8+MonoAndroid1+MonoTouch1"
#r "FSharp.Core.Fluent-4.0"
module WithFluent =
    open FSharp.Core.Fluent

    let histogram2 = 
        random3Dpoints
          .countBy(fun p -> int (norm p * 10.0 / sqrt 3.0) ) 
          .sortBy(fst)
          .toList()
//--> Added './packages/FSharp.Core.Fluent-4.0/lib/portable-net45+netcore45+wpa81+wp8+MonoAndroid1+MonoTouch1' to library include path
//
//
//--> Referenced '.\10Numeric\packages\FSharp.Core.Fluent-4.0\lib\portable-net45+netcore45+wpa81+wp8+MonoAndroid1+MonoTouch1\FSharp.Core.Fluent-4.0.dll'
//
//
//module WithFluent = begin
//  val histogram2 : (int * int) list =
//    [(0, 3); (1, 19); (2, 43); (3, 106); (4, 157); (5, 258); (6, 224);
//     (7, 148); (8, 36); (9, 6)]
//end

let rnd = System.Random()
let rand() = rnd.NextDouble()

/// Compute the variance of an array of inputs
let variance (values : float []) = 
    let sqr x = x * x
    let avg = values |> Array.average
    let sigma2 = values |> Array.averageBy (fun x -> sqr (x - avg))
    sigma2

let standardDeviation values =
    sqrt (variance values)

let sampleTimes = [|for x in 0 .. 1000 -> 50.0 + 10.0 * rand()|]

let exampleDeviation = standardDeviation sampleTimes
let exampleVariance = variance sampleTimes
//val rnd : System.Random
//val rand : unit -> float
//val variance : values:float [] -> float
//val standardDeviation : values:float [] -> float
//val sampleTimes : float [] =
//  [|50.74641475; 51.70919874; 57.80906234; 57.11545281; 55.80321544;
//    56.86958724; 50.70419731; 50.51275184; 50.84272924; 57.37302103;
//    58.47644749; 53.58564957; 55.151845; 59.74461252; 58.68181402; 53.04663817;
//    51.97964077; 50.32712458; 50.08940933; 52.23741942; 50.80705128;
//    59.98176568; 57.07222048; 54.08773938; 58.33433545; 58.50910997; 50.014769;
//    54.06053167; 57.29149157; 58.50522782; 53.18374214; 54.86680785;
//    55.23814487; 56.63441278; 50.92679013; 56.64328613; 52.09109524;
//    54.33612152; 53.05223345; 57.29510449; 56.57713554; 50.63283701;
//    52.14660801; 53.89385856; 54.21776902; 57.34032418; 54.35591769;
//    55.8039038; 52.39046785; 57.71443761; 57.54073505; 55.49845241;
//    51.72818693; 55.81907466; 51.34134382; 50.76464907; 54.63697827;
//    53.72132296; 58.78111735; 57.29410547; 56.85481825; 56.64366564;
//    53.22126027; 52.33750142; 54.18927889; 53.60963965; 58.3475047;
//    58.51743222; 58.81782239; 52.03852789; 50.95554293; 57.64351925;
//    57.27489114; 52.79430485; 55.66028388; 50.17421427; 57.83515767;
//    53.17836192; 59.86997036; 50.99401127; 54.15319228; 54.2108652;
//    51.67006383; 59.57705396; 50.96449277; 57.68528974; 53.13862092;
//    59.41907021; 55.29306896; 50.16214106; 52.00630786; 58.36977228;
//    55.55500417; 55.75812797; 50.44028624; 59.93346991; 57.41157674;
//    59.80910658; 59.70457967; 50.60812938; ...|]
//val exampleDeviation : float = 2.851213354
//val exampleVariance : float = 8.129417593

module Seq = 
    /// Compute the variance of the given statistic from from the input data
    let varianceBy (f : 'T -> float) values = 
        let sqr x = x * x
        let xs = values |> Seq.map f |> Seq.toArray
        let avg = xs |> Array.average
        let res = xs |> Array.averageBy (fun x -> sqr (x - avg))
        res

    /// Compute the standard deviation of the given statistic drawn from the input data
    let standardDeviationBy f values =
        sqrt (varianceBy f values)
//module Seq = begin
//  val varianceBy : f:('T -> float) -> values:seq<'T> -> float
//  val standardDeviationBy : f:('a -> float) -> values:seq<'a> -> float
//end

let inline variance values = 
    let sqr x = x * x
    let avg = values |> Array.average
    let sigma2 = values |> Array.averageBy (fun x -> sqr (x - avg))
    sigma2 

let inline standardDeviation values =
    sqrt (variance values)
//val inline variance :
//  values: ^a [] ->  ^c
//    when  ^a : (static member ( + ) :  ^a *  ^a ->  ^a) and
//          ^a : (static member DivideByInt :  ^a * int ->  ^a) and
//          ^a : (static member get_Zero : ->  ^a) and
//          ^a : (static member ( - ) :  ^a *  ^a ->  ^b) and
//          ^b : (static member ( * ) :  ^b *  ^b ->  ^c) and
//          ^c : (static member ( + ) :  ^c *  ^c ->  ^c) and
//          ^c : (static member DivideByInt :  ^c * int ->  ^c) and
//          ^c : (static member get_Zero : ->  ^c)
//val inline standardDeviation :
//  values: ^a [] ->  ^d
//    when  ^a : (static member ( + ) :  ^a *  ^a ->  ^a) and
//          ^a : (static member DivideByInt :  ^a * int ->  ^a) and
//          ^a : (static member get_Zero : ->  ^a) and
//          ^a : (static member ( - ) :  ^a *  ^a ->  ^b) and
//          ^b : (static member ( * ) :  ^b *  ^b ->  ^c) and
//          ^c : (static member ( + ) :  ^c *  ^c ->  ^c) and
//          ^c : (static member DivideByInt :  ^c * int ->  ^c) and
//          ^c : (static member get_Zero : ->  ^c) and
//          ^c : (static member Sqrt :  ^c ->  ^d)

type Input<'T> = {Data : 'T; Features : float[]}
type Centroid = float[]

module Array = 
    /// Like Seq.groupBy, but returns arrays 
    let classifyBy f (xs : _ []) = 
         xs |> Seq.groupBy f |> Seq.map (fun (k, v) -> (k, Seq.toArray v)) |> Seq.toArray

module Seq = 
    /// Return x, f(x), f(f(x)), f(f(f(x))), ...
    let iterate f x = x |> Seq.unfold (fun x -> Some (x, f x))

/// Compute the norm distance between an input and a centroid
let distance (xs : Input<_>) (ys : Centroid) =
    (xs.Features,ys) 
        ||> Array.map2 (fun x y -> (x - y) * (x - y))
        |> Array.sum

/// Find the average of set of inputs. First compute xs1 + ... + xsN, pointwise, 
/// then divide each element of the sum by the number of inputs.
let computeCentroidOfGroup (_, group : Input<_> []) =
    let e0 = group.[0].Features
    [|for i in 0 .. e0.Length - 1 -> group |> Array.averageBy (fun e -> e.Features.[i])|]

/// Group all the inputs by the nearest centroid
let classifyIntoGroups inputs centroids = 
    inputs |> Array.classifyBy (fun v -> centroids |> Array.minBy (distance v))

/// Repeatedly classify the inputs, starting with the initial centroids
let rec computeCentroids inputs centroids = seq {
    let classification = classifyIntoGroups inputs centroids
    yield classification
    let newCentroids = Array.map computeCentroidOfGroup classification
    yield! computeCentroids inputs newCentroids}

/// Extract the features and repeatedly classify the inputs, starting with the 
/// initial centroids
let kmeans inputs featureExtractor initialCentroids = 
    let inputs = 
        inputs 
        |> Seq.map (fun i -> {Data = i; Features = featureExtractor i}) 
        |> Seq.toArray
    let initialCentroids = initialCentroids |> Seq.toArray
    computeCentroids inputs initialCentroids

//type Input<'T> =
//  {Data: 'T;
//   Features: float [];}
//type Centroid = float []
//module Array = begin
//  val classifyBy :
//    f:('a -> 'b) -> xs:'a [] -> ('b * 'a []) [] when 'b : equality
//end
//module Seq = begin
//  val iterate : f:('a -> 'a) -> x:'a -> seq<'a>
//end
//val distance : xs:Input<'a> -> ys:Centroid -> float
//val computeCentroidOfGroup : 'a * group:Input<'b> [] -> float []
//val classifyIntoGroups :
//  inputs:Input<'a> [] -> centroids:Centroid [] -> (Centroid * Input<'a> []) []
//val computeCentroids :
//  inputs:Input<'a> [] ->
//    centroids:Centroid [] -> seq<(Centroid * Input<'a> []) []>
//val kmeans :
//  inputs:seq<'a> ->
//    featureExtractor:('a -> float []) ->
//      initialCentroids:seq<Centroid> -> seq<(Centroid * Input<'a> []) []>

open FSharp.Data.UnitSystems.SI.UnitSymbols

type Observation = {Time : float<s>; Location : float<m>}

let rnd = System.Random()
let rand() = rnd.NextDouble() 
let randZ() = rnd.NextDouble() - 0.5

/// Create a point near the given point
let near p = {Time= p.Time + randZ() * 20.0<s>; 
              Location = p.Location + randZ() * 5.0<m>}

let data = 
    [for i in 1 .. 1000 -> near {Time= 100.0<s>; Location = 60.0<m>}
     for i in 1 .. 1000 -> near {Time= 120.0<s>; Location = 80.0<m>}
     for i in 1 .. 1000 -> near {Time= 180.0<s>; Location = 30.0<m>}
     for i in 1 .. 1000 -> near {Time= 70.0<s>; Location = 40.0<m>}]

let maxTime = data |> Seq.maxBy (fun p -> p.Time) |> fun p -> p.Time
let maxLoc = data |> Seq.maxBy (fun p -> p.Location) |> fun p -> p.Location

let initialCentroids = [for i in 0 .. 9 -> [|rand(); rand()|]]
let featureExtractor (p : Observation) = [|p.Time / maxTime; p.Location / maxLoc|]

kmeans data featureExtractor initialCentroids
//type Observation =
//  {Time: float<s>;
//   Location: float<m>;}
//val rnd : System.Random
//val rand : unit -> float
//val randZ : unit -> float
//val near : p:Observation -> Observation
//val data : Observation list =
//  [{Time = 105.277029;
//    Location = 58.47062483;}; {Time = 98.25503244;
//                               Location = 62.3196129;};
//   {Time = 91.77732868;
//    Location = 60.89619981;}; {Time = 93.38437921;
//                               Location = 57.76320545;};
//   {Time = 95.34047188;
//    Location = 62.15150421;}; {Time = 103.5614007;
//                               Location = 61.99827869;};
//   {Time = 96.20759813;
//    Location = 61.03048641;}; {Time = 93.15709541;
//                               Location = 59.01728845;};
//   {Time = 102.3049272;
//    Location = 58.69203883;}; {Time = 97.81975074;
//                               Location = 58.63638913;};
//   {Time = 96.45128151;
//    Location = 62.4863197;}; {Time = 105.8830965;
//                              Location = 61.59881342;};
//   {Time = 105.4284323;
//    Location = 59.76259152;}; {Time = 105.1421715;
//                               Location = 60.55704843;};
//   {Time = 92.95965855;
//    Location = 57.63532598;}; {Time = 102.3492717;
//                               Location = 61.86701633;};
//   {Time = 105.1426213;
//    Location = 59.54941726;}; {Time = 105.2885616;
//                               Location = 58.25570226;};
//   {Time = 94.91260562;
//    Location = 59.86664617;}; {Time = 108.762249;
//                               Location = 60.82521153;};
//   {Time = 109.2778799;
//    Location = 58.52473662;}; {Time = 91.56123427;
//                               Location = 60.98863068;};
//   {Time = 102.4508382;
//    Location = 60.43895069;}; {Time = 105.7316047;
//                               Location = 58.26352494;};
//   {Time = 108.1337925;
//    Location = 60.61965064;}; {Time = 98.65961407;
//                               Location = 61.90366198;};
//   {Time = 107.1223586;
//    Location = 62.49715002;}; {Time = 108.6585457;
//                               Location = 61.33293755;};
//   {Time = 97.99940279;
//    Location = 60.46494469;}; {Time = 93.85001932;
//                               Location = 60.68174065;};
//   {Time = 108.4426277;
//    Location = 60.28904637;}; {Time = 108.0931632;
//                               Location = 58.69979199;};
//   {Time = 96.2567451;
//    Location = 61.52333385;}; {Time = 92.85049345;
//                               Location = 62.00248227;};
//   {Time = 108.8333841;
//    Location = 57.53357159;}; {Time = 91.15654818;
//                               Location = 58.20958562;};
//   {Time = 96.00590634;
//    Location = 61.12972616;}; {Time = 95.26767661;
//                               Location = 58.08808376;};
//   {Time = 108.3840446;
//    Location = 57.98214345;}; {Time = 93.9444155;
//                               Location = 58.41815739;};
//   {Time = 103.3187614;
//    Location = 60.52201793;}; {Time = 104.0944012;
//                               Location = 60.120264;};
//   {Time = 101.8816898;
//    Location = 61.18365596;}; {Time = 90.34570673;
//                               Location = 61.28850531;};
//   {Time = 99.5391233;
//    Location = 62.48920284;}; {Time = 105.0234062;
//                               Location = 60.76320672;};
//   {Time = 95.61656537;
//    Location = 59.0088216;}; {Time = 104.8582184;
//                              Location = 59.5304236;};
//   {Time = 96.00578322;
//    Location = 61.69051657;}; {Time = 97.6977776;
//                               Location = 61.58937569;};
//   {Time = 98.9053093;
//    Location = 61.9304189;}; {Time = 94.22071567;
//                              Location = 61.99987654;};
//   {Time = 101.3220544;
//    Location = 58.95531789;}; {Time = 101.6087416;
//                               Location = 58.15086349;};
//   {Time = 104.7209235;
//    Location = 61.57655267;}; {Time = 106.9477056;
//                               Location = 59.01770724;};
//   {Time = 97.91536326;
//    Location = 57.54434745;}; {Time = 109.4082012;
//                               Location = 59.088639;};
//   {Time = 107.0617843;
//    Location = 59.40302681;}; {Time = 102.9174781;
//                               Location = 60.38053032;};
//   {Time = 105.7476287;
//    Location = 59.42411805;}; {Time = 98.47080579;
//                               Location = 62.21914318;};
//   {Time = 95.11088012;
//    Location = 59.52593033;}; {Time = 107.2217771;
//                               Location = 62.49265499;};
//   {Time = 109.6606863;
//    Location = 61.78649555;}; {Time = 106.3465518;
//                               Location = 57.90549456;};
//   {Time = 104.2307962;
//    Location = 59.39672818;}; {Time = 104.7673202;
//                               Location = 61.39927145;};
//   {Time = 97.86735607;
//    Location = 61.52372375;}; {Time = 109.1590016;
//                               Location = 59.01510457;};
//   {Time = 93.12588227;
//    Location = 61.93556319;}; {Time = 90.43309781;
//                               Location = 60.80822815;};
//   {Time = 93.00910574;
//    Location = 59.73814432;}; {Time = 95.13746361;
//                               Location = 58.8597939;};
//   {Time = 96.6270852;
//    Location = 59.62591559;}; {Time = 101.0599101;
//                               Location = 62.09841899;};
//   {Time = 93.84458817;
//    Location = 61.54391408;}; {Time = 90.60987407;
//                               Location = 57.80220928;};
//   {Time = 99.2508698;
//    Location = 58.83603573;}; {Time = 102.8886261;
//                               Location = 58.30458327;};
//   {Time = 98.59949446;
//    Location = 60.4095304;}; {Time = 92.94276769;
//                              Location = 61.89373531;};
//   {Time = 109.9596589;
//    Location = 61.33143184;}; {Time = 101.8400328;
//                               Location = 57.58211263;};
//   {Time = 95.41006955;
//    Location = 58.45277885;}; {Time = 108.4871999;
//                               Location = 57.74172233;};
//   {Time = 98.45310565;
//    Location = 59.21426497;}; {Time = 98.396239;
//                               Location = 62.00134398;};
//   {Time = 97.26337438;
//    Location = 61.3094733;}; {Time = 105.867467;
//                              Location = 61.53957571;};
//   {Time = 92.96625771;
//    Location = 60.44565038;}; {Time = 103.3435347;
//                               Location = 60.28925597;};
//   {Time = 96.08607215;
//    Location = 61.98821896;}; {Time = 107.7773901;
//                               Location = 62.01378497;};
//   {Time = 96.97703867;
//    Location = 60.88962078;}; {Time = 96.346216;
//                               Location = 58.13080328;};
//   {Time = 93.20626894;
//    Location = 61.48516713;}; {Time = 107.4609238;
//                               Location = 60.37194017;};
//   {Time = 104.7994851;
//    Location = 58.21453914;}; {Time = 103.2732537;
//                               Location = 59.42084459;}; ...]
//val maxTime : float<s> = 189.9999648
//val maxLoc : float<m> = 82.49924309
//val initialCentroids : float [] list =
//  [[|0.1960786573; 0.6383903207|]; [|0.4439405829; 0.898303614|];
//   [|0.4044785478; 0.7751061985|]; [|0.4884978298; 0.5526476095|];
//   [|0.8503274726; 0.4777164084|]; [|0.8347954423; 0.3467332573|];
//   [|0.5529799944; 0.9996279082|]; [|0.738157166; 0.7593231633|];
//   [|0.6645418921; 0.9854628039|]; [|0.8079144251; 0.1606845833|]]
//val featureExtractor : p:Observation -> float []
//val it : seq<(Centroid * Input<Observation> []) []>

kmeans data featureExtractor initialCentroids
   |> Seq.map (Array.map (fun (c, _) -> c.[0] * maxTime, c.[1] * maxLoc))
   |> Seq.item 100 
//val it : (float<s> * float<m>) [] =
//  [|(105.3269046, 60.01263184); (95.30766069, 60.08878118);
//    (115.2753112, 79.9586516); (125.2015779, 80.11626901);
//    (180.029382, 29.95317314); (74.83114243, 40.06720622);
//    (64.88315234, 39.96968837)|]

#I "./packages/MathNet.Numerics/lib/net40"
#I "./packages/MathNet.Numerics.FSharp/lib/net40"

#r "MathNet.Numerics"
#r "MathNet.Numerics.FSharp"

open MathNet.Numerics.Statistics 

let data = [for i in 0.0 .. 0.01 .. 10.0 -> sin i] 

let exampleVariance = data |> Statistics.Variance 
let exampleMean = data |> Statistics.Mean 
let exampleMin = data |> Statistics.Minimum
let exampleMax = data |> Statistics.Maximum
//--> Added './packages/MathNet.Numerics/lib/net40' to library include path
//
//
//--> Added './packages/MathNet.Numerics.FSharp/lib/net40' to library include path
//
//
//--> Referenced '.\packages\MathNet.Numerics\lib\net40\MathNet.Numerics.dll'
//
//
//--> Referenced '.\packages\MathNet.Numerics.FSharp\lib\net40\MathNet.Numerics.FSharp.dll'
//
//
//val data : float list =
//  [0.0; 0.009999833334; 0.01999866669; 0.0299955002; 0.03998933419;
//   0.04997916927; 0.05996400648; 0.06994284734; 0.07991469397; 0.0898785492;
//   0.09983341665; 0.1097783008; 0.1197122073; 0.1296341426; 0.1395431146;
//   0.1494381325; 0.1593182066; 0.1691823491; 0.1790295734; 0.188858895;
//   0.1986693308; 0.2084598998; 0.2182296231; 0.2279775235; 0.2377026264;
//   0.2474039593; 0.2570805519; 0.2667314367; 0.2763556486; 0.2859522251;
//   0.2955202067; 0.3050586364; 0.3145665606; 0.3240430284; 0.3334870921;
//   0.3428978075; 0.3522742333; 0.361615432; 0.3709204694; 0.3801884151;
//   0.3894183423; 0.398609328; 0.4077604531; 0.4168708024; 0.4259394651;
//   0.4349655341; 0.443948107; 0.4528862854; 0.4617791755; 0.4706258882;
//   0.4794255386; 0.4881772469; 0.4968801378; 0.5055333412; 0.5141359917;
//   0.5226872289; 0.5311861979; 0.5396320487; 0.5480239368; 0.5563610229;
//   0.5646424734; 0.5728674601; 0.5810351605; 0.5891447579; 0.5971954414;
//   0.6051864057; 0.613116852; 0.620985987; 0.628793024; 0.6365371822;
//   0.6442176872; 0.651833771; 0.659384672; 0.666869635; 0.6742879116;
//   0.68163876; 0.6889214451; 0.6961352386; 0.7032794192; 0.7103532724;
//   0.7173560909; 0.7242871744; 0.7311458297; 0.7379313711; 0.74464312;
//   0.7512804051; 0.7578425629; 0.764328937; 0.7707388789; 0.7770717475;
//   0.7833269096; 0.7895037397; 0.79560162; 0.8016199409; 0.8075581004;
//   0.8134155048; 0.8191915683; 0.8248857133; 0.8304973705; 0.8360259786; ...]
//val exampleVariance : float = 0.443637494
//val exampleMean : float = 0.1834501596
//val exampleMin : float = -0.9999971464
//val exampleMax : float = 0.9999996829

#I "./packages/MathNet.Numerics/lib/net40"
#I "./packages/MathNet.Numerics.FSharp/lib/net40"

#r "MathNet.Numerics"
#r "MathNet.Numerics.FSharp"
#load "packages/FSharp.Charting/FSharp.Charting.fsx"

open FSharp.Charting
open MathNet.Numerics.Statistics 
open MathNet.Numerics.Distributions 
open System.Collections.Generic

let exampleBellCurve = Normal(100.0, 10.0)
exampleBellCurve.Samples()
////val it : IEnumerable<float> =
//  seq [99.8179578; 93.878374; 96.62167258; 104.579042; ...]

let histogram n data = 
    let h = Histogram(data, n)
    [|for i in 0 .. h.BucketCount - 1 -> 
          (sprintf "%.0f-%.0f" h.[i].LowerBound h.[i].UpperBound, h.[i].Count)|]

exampleBellCurve.Samples() 
    |> Seq.truncate 1000 
    |> histogram 10 
    |> Chart.Column
//val histogram : n:int -> data:IEnumerable<float> -> (string * float) []
//val it : ChartTypes.GenericChart = (Chart)

#I "./packages/MathNet.Numerics/lib/net40"
#I "./packages/MathNet.Numerics.FSharp/lib/net40"

#r "MathNet.Numerics"
#r "MathNet.Numerics.FSharp"

open MathNet.Numerics
open MathNet.Numerics.LinearAlgebra

let vector1 = vector [1.0; 2.4; 3.0]
let vector2 = vector [7.0; 2.1; 5.4]
//val vector1 : Vector<float>
//val vector2 : Vector<float>

vector1 + vector2
//val it : Vector<float> = seq [8.0; 4.5; 8.4]

let matrix1 = matrix [[1.0; 2.0]; [1.0; 3.0]]
let matrix2 = matrix [[1.0; -2.0]; [0.5; 3.0]]
let ``matrix1*2`` = matrix1 * matrix2
//val matrix1 : Matrix<float> = DenseMatrix 2x2-Double
//1  2
//1  3
//
//val matrix2 : Matrix<float> = DenseMatrix 2x2-Double
//  1  -2
//0.5   3
//
//val ( matrix1*2 ) : Matrix<float> = DenseMatrix 2x2-Double
//  2  4
//2.5  7

open MathNet.Numerics.LinearAlgebra.Double 

fsi.AddPrintTransformer (fun (x : DenseVector) -> 
     box [|for i in 0 .. x.Count - 1 -> x.[i]|])

fsi.AddPrintTransformer (fun (x : DenseMatrix) -> 
     box (array2D [for i in 0 .. x.RowCount - 1 -> 
                       [for j in 0 .. x.ColumnCount - 1 -> x.[i, j]]]))

//val vector1 : Vector<float> = [|1.0; 2.4; 3.0|]
//val vector2 : Vector<float> = [|7.0; 2.1; 5.4|]
//val it : Vector<float> = [|8.0; 4.5; 8.4|]
//val matrix1 : Matrix<float> = [[1.0; 2.0]
//                               [1.0; 3.0]]
//val matrix2 : Matrix<float> = [[1.0; -2.0]
//                               [0.5; 3.0]]
//val ( matrix1*2 ) : Matrix<float> = [[2.0; 4.0]
//                                     [2.5; 7.0]]

let rnd = System.Random()
let rand() = rnd.NextDouble()

let largeMatrix = matrix [for i in 1 .. 100 -> [for j in 1 .. 100 -> rand()]]

let inverse = largeMatrix.Inverse()
let check = largeMatrix * largeMatrix.Inverse()
//val check : Matrix<float> =
//  [[1.0; -8.326672685e-17; -1.165734176e-15; 1.221245327e-15; 7.549516567e-15;
//    -9.992007222e-16; -6.661338148e-16; 8.049116929e-16; -2.164934898e-15;

open MathNet.Numerics.LinearAlgebra.Double
open MathNet.Numerics.LinearAlgebra.Factorization

let evd = largeMatrix.Evd()
let eigenValues = evd.EigenValues
//val evd : Evd<float>
//val eigenValues : Vector<System.Numerics.Complex> =
//  seq
//    [(50.2346779458135, 0) {Imaginary = 0.0;
//                            Magnitude = 50.23467795;
//                            Phase = 0.0;
//                            Real = 50.23467795;};
//     (0.861101482585932, 2.89427431636702) {Imaginary = 2.894274316;
//                                            Magnitude = 3.01965554;
//                                            Phase = 1.281617268;
//                                            Real = 0.8611014826;};
//     (0.861101482585932, -2.89427431636702) {Imaginary = -2.894274316;
//                                             Magnitude = 3.01965554;
//                                             Phase = -1.281617268;
//                                             Real = 0.8611014826;};
//     (1.80283045021777, 2.29691370496494) {Imaginary = 2.296913705;
//                                           Magnitude = 2.919933253;
//                                           Phase = 0.9053353699;
//                                           Real = 1.80283045;}; ...]

fsi.AddPrinter (fun (c : System.Numerics.Complex) -> sprintf "%fr + %fi" c.Real c.Imaginary)
//val eigenValues : Vector<System.Numerics.Complex> =
//  seq
//    [50.304146r + 0.000000i; -3.426402r + 0.000000i; 1.880429r + 2.054711i;
//     1.880429r + -2.054711i; ...]


[<Measure>] 
type click

[<Measure>] 
type pixel

[<Measure>] 
type money

open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols

let rateOfClicks = 200.0<click/s>
let durationOfExecution = 3.5<s>
let numberOfClicks = rateOfClicks * durationOfExecution 
//[<Measure>]
//type click
//[<Measure>]
//type pixel
//[<Measure>]
//type money
//val rateOfClicks : float<click/Data.UnitSystems.SI.UnitSymbols.s> = 200.0
//val durationOfExecution : float<Data.UnitSystems.SI.UnitSymbols.s> = 3.5
//val numberOfClicks : float<click> = 700.0

let integrateByMidpointRule f (a, b) = (b - a) * f ((a + b) / 2.0)

let integrateByTrapezoidalRule f (a, b) = (b - a) * ((f a + f b) / 2.0)

let integrateByIterativeRule f (a, b) n = 
    (b - a) / float n * 
    ((f a + f b) / 2.0 + 
      List.sum [for k in 1 .. n - 1 -> f (a + float k * (b - a) / float n)])
//val integrateByMidpointRule : f:(float -> float) -> a:float * b:float -> float
//val integrateByTrapezoidalRule :
//  f:(float -> float) -> a:float * b:float -> float
//val integrateByIterativeRule :
//  f:(float -> float) -> a:float * b:float -> n:int -> float

let integrateByMidpointRule (f : float<'u> -> float<'v>) (a : float<'u>, b : float<'u>) = 
    (b - a) * f ( (a+b) / 2.0)

let integrateByTrapezoidalRule (f : float<'u> -> float<'v>) (a : float<'u>, b : float<'u>) = 
    (b - a) * ((f a + f b) / 2.0)

let integrateByIterativeRule (f : float<'u> -> float<'v>) (a : float<'u>, b : float<'u>) n = 
    (b - a) / float n * 
    ((f a + f b) / 2.0 + 
      List.sum [for k in 1 .. n - 1 -> f (a + float k * (b - a) / float n)])
//val integrateByMidpointRule :
//  f:(float<'u> -> float<'v>) -> a:float<'u> * b:float<'u> -> float<'u 'v>
//val integrateByTrapezoidalRule :
//  f:(float<'u> -> float<'v>) -> a:float<'u> * b:float<'u> -> float<'u 'v>
//val integrateByIterativeRule :
//  f:(float<'u> -> float<'v>) ->
//    a:float<'u> * b:float<'u> -> n:int -> float<'u 'v>

let velocityFunction (t : float<s>) = 100.0<m/s> + t * -9.81<m/s^2>

let distance1 = integrateByMidpointRule velocityFunction (0.0<s>, 10.0<s>)
let distance2 = integrateByTrapezoidalRule velocityFunction (0.0<s>, 10.0<s>)
let distance3 = integrateByIterativeRule velocityFunction (0.0<s>, 10.0<s>) 10
//val velocityFunction : t:float<s> -> float<m/s>
//val distance1 : float<m> = 509.5
//val distance2 : float<m> = 509.5
//val distance3 : float<m> = 509.5

let rnd = System.Random()
let rand() = rnd.NextDouble()

let variance (values : seq<float<_>>) = 
    let sqr x = x * x
    let xs = values |> Seq.toArray
    let avg = xs |> Array.average
    let variance = xs |> Array.averageBy (fun x -> sqr (x - avg))
    variance

let standardDeviation values = sqrt (variance values)
let sampleTimes = [for x in 0 .. 1000 -> 50.0<s> + 10.0<s> * rand()]
let exampleDeviation = standardDeviation sampleTimes
let exampleVariance = variance sampleTimes
//val rnd : System.Random
//val rand : unit -> float
//val variance : values:seq<float<'u>> -> float<'u ^ 2>
//val standardDeviation : values:seq<float<'u>> -> float<'u>
//val sampleTimes : float<s> list =
//  [58.1915264; 51.69513363; 53.87450026; 56.33507186; 56.36183672; 58.37867189;
//   51.41112413; 54.34990102; 59.71174505; 58.58001255; 50.64222805;
//   52.47863044; 54.13611422; 51.39133659; 51.73809469; 53.63780776; 51.7495425;
//   58.45551622; 50.66664876; 52.52724603; 51.15095152; 50.1705179; 50.07699784;
//   55.05177605; 55.27857246; 59.60431476; 57.39142337; 58.89429333;
//   54.49803317; 57.12200967; 50.94758233; 55.38034288; 55.97357148;
//   50.01616867; 54.17446851; 56.22892293; 52.29533613; 57.56733944; 50.5132353;
//   51.80061377; 57.59620276; 51.51974052; 50.88328285; 56.38682661;
//   53.52091317; 57.93121884; 53.78029786; 50.25167997; 52.23870189;
//   57.82879238; 52.56512773; 56.84794948; 51.60903109; 56.55140395;
//   53.26444453; 58.02100849; 51.61813579; 58.82272421; 51.05649941;
//   56.75752196; 50.98724852; 52.5168308; 59.85186785; 52.58973538; 57.63243023;
//   55.26188517; 56.50505896; 54.11994555; 57.21686808; 55.50917177;
//   51.34247163; 54.18220306; 57.94228093; 58.866035; 54.93104327; 59.631211;
//   59.28723506; 53.69017122; 51.53086288; 57.34735362; 55.8240169; 57.13974341;
//   56.65559143; 56.66924079; 54.55688195; 54.09963284; 53.77131179;
//   59.42216753; 56.75172414; 56.15346002; 54.61078713; 53.47261192;
//   56.51084003; 53.75571334; 50.81336525; 55.07937196; 51.66787268;
//   58.29354747; 58.75439639; 58.259028; ...]
//val exampleDeviation : float<s> = 2.881077324
//val exampleVariance : float<s ^ 2> = 8.300606545

type Vector2D<[<Measure>] 'u> = {DX : float<'u>; DY : float<'u>}
//type Vector2D<'u> =
//  {DX: float<'u>;
//   DY: float<'u>;}

/// Two-dimensional vectors
type Vector2D<[<Measure>] 'u>(dx : float<'u>, dy : float<'u>) =

    /// Get the X component of the vector
    member v.DX = dx    
    
    /// Get the Y component of the vector
    member v.DY = dy    
    
    /// Get the length of the vector
    member v.Length = sqrt(dx * dx + dy * dy)
    
    /// Get a vector scaled by the given factor
    member v.Scale k = Vector2D(k * dx, k * dy)
    
    /// Return a vector shifted by the given delta in the X coordinate
    member v.ShiftX x = Vector2D(dx + x, dy)

    /// Return a vector shifted by the given delta in the Y coordinate
    member v.ShiftY y = Vector2D(dx, dy + y)

    /// Get the zero vector
    static member Zero = Vector2D<'u>(0.0<_>, 0.0<_>)

    /// Return a constant vector along the X axis
    static member ConstX dx = Vector2D<'u>(dx, 0.0<_>)

    /// Return a constant vector along the Y axis
    static member ConstY dy = Vector2D<'u>(0.0<_>, dy)

    /// Return the sum of two vectors
    static member (+) (v1 : Vector2D<'u>, v2 : Vector2D<'u>) = 
        Vector2D(v1.DX + v2.DX, v1.DY + v2.DY)

    /// Return the difference of two vectors
    static member (-) (v1 : Vector2D<'u>, v2 : Vector2D<'u>) = 
        Vector2D(v1.DX - v2.DX, v1.DY - v2.DY)

    /// Return the pointwise-product of two vectors
    static member (.*) (v1 : Vector2D<'u>, v2 : Vector2D<'u>) = 
        Vector2D(v1.DX * v2.DX, v1.DY * v2.DY)
//type Vector2D<'u> =
//  class
//    new : dx:float<'u> * dy:float<'u> -> Vector2D<'u>
//    member Scale : k:float<'u0> -> Vector2D<'u 'u0>
//    member ShiftX : x:float<'u> -> Vector2D<'u>
//    member ShiftY : y:float<'u> -> Vector2D<'u>
//    member DX : float<'u>
//    member DY : float<'u>
//    member Length : float<'u>
//    static member ConstX : dx:float<'u> -> Vector2D<'u>
//    static member ConstY : dy:float<'u> -> Vector2D<'u>
//    static member Zero : Vector2D<'u>
//    static member ( + ) : v1:Vector2D<'u> * v2:Vector2D<'u> -> Vector2D<'u>
//    static member
//      ( .* ) : v1:Vector2D<'u> * v2:Vector2D<'u> -> Vector2D<'u ^ 2>
//    static member ( - ) : v1:Vector2D<'u> * v2:Vector2D<'u> -> Vector2D<'u>
//  end

let three = float 3.0<kg>
let sixKg = LanguagePrimitives.FloatWithMeasure<kg> (three + three)
//val three : float = 3.0
//val sixKg : float<kg> = 6.0

type Input<'T, [<Measure>] 'u> = 
    {Data : 'T 
     Features: float<'u>[]}
//type Input<'T,'u> =
//  {Data: 'T;
//   Features: float<'u> [];}

type Centroid<[<Measure>] 'u> = float<'u>[]
//type Centroid<'u> = float<'u> []