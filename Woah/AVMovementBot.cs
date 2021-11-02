using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Woah
{
    class AVMovementBot : WoahBotBase
    {
        public MenuItem Enabledcheck;


        Waypoint currentwaypoint = new Waypoint();
        WaypointList StartToVann = new WaypointList("StartToVann");
        WaypointList VannsNoAFK = new WaypointList("VannsNoAFK");
        WaypointList RHToVann = new WaypointList("RHToVann");
        WaypointList TopGYToVann = new WaypointList("TopGYToVann");

        WaypointList StartToEnd = new WaypointList("StartToEnd");
        WaypointList EndCircle = new WaypointList("EndCircle");
        WaypointList TopGYToEnd = new WaypointList("TopGYToEnd");
        WaypointList STToEnd = new WaypointList("STToEnd");
        WaypointList IBToEnd = new WaypointList("IBToEnd");
        WaypointList SFToEnd = new WaypointList("SFToEnd");

        WaypointList waypoints = null;

        public AVMovementBot(WoahEnvironment env, WoahFish theform)
            : base(env, theform)
        {
            theform.HookKeyPress += new myKeyEventHandler(onKeyEvent);

            StartToVann.Add(new Vector3(-492.0635f, 872.2219f, 96.55447f));
            StartToVann.Add(new Vector3(-492.4164f, 871.869f, 96.56015f));
            StartToVann.Add(new Vector3(-493.8098f, 870.3861f, 96.58231f));
            StartToVann.Add(new Vector3(-495.0443f, 868.4064f, 96.59972f));
            StartToVann.Add(new Vector3(-495.8811f, 866.6834f, 96.57005f));
            StartToVann.Add(new Vector3(-496.8091f, 864.5677f, 96.43065f));
            StartToVann.Add(new Vector3(-497.6116f, 862.251f, 96.29126f));
            StartToVann.Add(new Vector3(-498.0474f, 860.0641f, 96.31773f));
            StartToVann.Add(new Vector3(-498.336f, 857.756f, 96.38161f));
            StartToVann.Add(new Vector3(-498.4081f, 854.9881f, 96.50004f));
            StartToVann.Add(new Vector3(-498.4391f, 852.4327f, 96.65147f));
            StartToVann.Add(new Vector3(-498.3542f, 849.7213f, 97.1773f));
            StartToVann.Add(new Vector3(-498.2101f, 846.9801f, 97.94147f));
            StartToVann.Add(new Vector3(-497.8657f, 842.5983f, 98.42051f));
            StartToVann.Add(new Vector3(-497.6686f, 840.0944f, 98.67544f));
            StartToVann.Add(new Vector3(-497.4785f, 837.6789f, 98.95889f));
            StartToVann.Add(new Vector3(-497.2777f, 835.1269f, 99.29561f));
            StartToVann.Add(new Vector3(-497.073f, 832.5267f, 99.68948f));
            StartToVann.Add(new Vector3(-496.8621f, 829.8463f, 100.0955f));
            StartToVann.Add(new Vector3(-496.6461f, 827.1017f, 100.3736f));
            StartToVann.Add(new Vector3(-496.4642f, 824.7904f, 100.5837f));
            StartToVann.Add(new Vector3(-496.2671f, 822.2866f, 100.6307f));
            StartToVann.Add(new Vector3(-496.0454f, 819.4697f, 100.3965f));
            StartToVann.Add(new Vector3(-495.8244f, 816.6609f, 100.1825f));
            StartToVann.Add(new Vector3(-495.6134f, 813.9805f, 100.1065f));
            StartToVann.Add(new Vector3(-495.4056f, 811.3402f, 100.1135f));
            StartToVann.Add(new Vector3(-495.1884f, 808.5795f, 100.1209f));
            StartToVann.Add(new Vector3(-494.9843f, 805.9874f, 100.0056f));
            StartToVann.Add(new Vector3(-494.7759f, 803.3391f, 99.84214f));
            StartToVann.Add(new Vector3(-494.5555f, 800.5383f, 99.66915f));
            StartToVann.Add(new Vector3(-494.0721f, 795.4415f, 99.74665f));
            StartToVann.Add(new Vector3(-493.8141f, 792.8218f, 99.6375f));
            StartToVann.Add(new Vector3(-493.5703f, 790.3463f, 99.45544f));
            StartToVann.Add(new Vector3(-493.3123f, 787.7266f, 99.34443f));
            StartToVann.Add(new Vector3(-493.0716f, 785.2832f, 99.53984f));
            StartToVann.Add(new Vector3(-492.8286f, 782.8158f, 99.72604f),WaypointAction.Mount);
            StartToVann.Add(new Vector3(-492.5824f, 780.3162f, 99.91476f));
            StartToVann.Add(new Vector3(-492.3228f, 777.6805f, 99.57999f));
            StartToVann.Add(new Vector3(-492.1176f, 775.5976f, 99.06631f));
            StartToVann.Add(new Vector3(-491.8391f, 772.7697f, 98.369f));
            StartToVann.Add(new Vector3(-491.6103f, 770.4464f, 97.77317f));
            StartToVann.Add(new Vector3(-491.3263f, 767.5624f, 97.7298f));
            StartToVann.Add(new Vector3(-491.0764f, 765.0256f, 97.56387f));
            StartToVann.Add(new Vector3(-490.5489f, 760.1829f, 96.75787f));
            StartToVann.Add(new Vector3(-489.4833f, 755.8158f, 94.96753f));
            StartToVann.Add(new Vector3(-487.1319f, 750.6495f, 92.00976f));
            StartToVann.Add(new Vector3(-484.7589f, 745.6577f, 88.62697f));
            StartToVann.Add(new Vector3(-482.9846f, 741.9251f, 86.23557f));
            StartToVann.Add(new Vector3(-481.304f, 738.3898f, 84.52072f));
            StartToVann.Add(new Vector3(-479.8904f, 735.416f, 83.12684f));
            StartToVann.Add(new Vector3(-478.4045f, 732.2903f, 81.72768f));
            StartToVann.Add(new Vector3(-477.082f, 729.5149f, 80.56645f));
            StartToVann.Add(new Vector3(-475.5433f, 726.3963f, 79.12934f));
            StartToVann.Add(new Vector3(-473.8765f, 723.1541f, 77.58145f));
            StartToVann.Add(new Vector3(-472.1924f, 720.1504f, 75.91499f));
            StartToVann.Add(new Vector3(-470.1932f, 717.1039f, 73.93932f));
            StartToVann.Add(new Vector3(-468.0716f, 714.4557f, 72.06284f));
            StartToVann.Add(new Vector3(-465.7591f, 711.8597f, 70.06896f));
            StartToVann.Add(new Vector3(-463.1437f, 709.2724f, 68.06211f));
            StartToVann.Add(new Vector3(-460.4848f, 706.853f, 66.29922f));
            StartToVann.Add(new Vector3(-457.9687f, 704.9744f, 65.06242f));
            StartToVann.Add(new Vector3(-455.0529f, 703.3474f, 64.1478f));
            StartToVann.Add(new Vector3(-451.3832f, 701.9536f, 63.30923f));
            StartToVann.Add(new Vector3(-448.7755f, 701.1176f, 62.9081f));
            StartToVann.Add(new Vector3(-440.5525f, 698.4815f, 62.73186f));
            StartToVann.Add(new Vector3(-437.4036f, 697.472f, 62.73836f));
            StartToVann.Add(new Vector3(-433.0201f, 696.0668f, 62.74926f));
            StartToVann.Add(new Vector3(-429.7086f, 695.0051f, 62.83706f));
            StartToVann.Add(new Vector3(-425.837f, 693.764f, 63.28435f));
            StartToVann.Add(new Vector3(-422.2855f, 692.6254f, 63.85691f));
            StartToVann.Add(new Vector3(-418.7019f, 691.4766f, 64.45986f));
            StartToVann.Add(new Vector3(-414.7824f, 690.22f, 65.24052f));
            StartToVann.Add(new Vector3(-410.9109f, 688.9789f, 66.09328f));
            StartToVann.Add(new Vector3(-404.4276f, 687.0292f, 65.75336f));
            StartToVann.Add(new Vector3(-400.4463f, 685.9853f, 63.42781f));
            StartToVann.Add(new Vector3(-396.5449f, 684.9673f, 61.75859f));
            StartToVann.Add(new Vector3(-392.611f, 683.9407f, 60.48877f));
            StartToVann.Add(new Vector3(-389.2949f, 683.0754f, 58.54076f));
            StartToVann.Add(new Vector3(-385.0522f, 681.9683f, 54.87781f));
            StartToVann.Add(new Vector3(-377.8997f, 680.1019f, 45.72765f));
            StartToVann.Add(new Vector3(-374.3397f, 679.1729f, 39.78131f));
            StartToVann.Add(new Vector3(-370.7472f, 678.2355f, 32.84274f));
            StartToVann.Add(new Vector3(-367.5279f, 677.3954f, 29.78076f));
            StartToVann.Add(new Vector3(-364.0526f, 676.4749f, 29.78076f));
            StartToVann.Add(new Vector3(-360.8179f, 675.2216f, 29.78076f));
            StartToVann.Add(new Vector3(-357.883f, 673.459f, 29.87659f));
            StartToVann.Add(new Vector3(-355.3679f, 670.8693f, 29.64349f));
            StartToVann.Add(new Vector3(-353.0082f, 668.3608f, 29.24791f));
            StartToVann.Add(new Vector3(-350.2093f, 666.0317f, 29.34349f));
            StartToVann.Add(new Vector3(-346.766f, 664.3412f, 29.77711f));
            StartToVann.Add(new Vector3(-343.5668f, 664.2436f, 29.81228f));
            StartToVann.Add(new Vector3(-339.3639f, 664.7367f, 29.82463f));
            StartToVann.Add(new Vector3(-335.4885f, 665.2883f, 29.9786f));
            StartToVann.Add(new Vector3(-331.4635f, 665.8611f, 30.06313f));
            StartToVann.Add(new Vector3(-327.8784f, 666.2821f, 30.21487f));
            StartToVann.Add(new Vector3(-322.8127f, 666.0956f, 30.38618f));
            StartToVann.Add(new Vector3(-320.0231f, 665.6774f, 30.31136f));
            StartToVann.Add(new Vector3(-316.6343f, 664.8251f, 30.52865f));
            StartToVann.Add(new Vector3(-313.2688f, 663.9495f, 30.19884f));
            StartToVann.Add(new Vector3(-308.7611f, 663.4136f, 29.73362f));
            StartToVann.Add(new Vector3(-305.7065f, 663.2793f, 29.84857f));
            StartToVann.Add(new Vector3(-301.93f, 663.116f, 30.11359f));
            StartToVann.Add(new Vector3(-298.2472f, 662.92f, 30.28808f));
            StartToVann.Add(new Vector3(-295.0606f, 662.199f, 30.27106f));
            StartToVann.Add(new Vector3(-291.7839f, 660.5377f, 30.13939f));
            StartToVann.Add(new Vector3(-288.9005f, 658.2657f, 30.08135f));
            StartToVann.Add(new Vector3(-286.2964f, 655.7815f, 30.21236f));
            StartToVann.Add(new Vector3(-283.4027f, 653.0145f, 30.65711f));
            StartToVann.Add(new Vector3(-280.7506f, 650.4645f, 30.97097f));
            StartToVann.Add(new Vector3(-278.1832f, 647.996f, 30.99376f));
            StartToVann.Add(new Vector3(-275.4705f, 645.3878f, 30.79733f));
            StartToVann.Add(new Vector3(-272.3569f, 642.6486f, 30.49635f));
            StartToVann.Add(new Vector3(-269.3817f, 640.5177f, 30.096f));
            StartToVann.Add(new Vector3(-265.7377f, 638.6442f, 31.08957f));
            StartToVann.Add(new Vector3(-262.3318f, 637.0442f, 31.99838f));
            StartToVann.Add(new Vector3(-258.9277f, 635.7894f, 32.8066f));
            StartToVann.Add(new Vector3(-255.8426f, 634.9729f, 33.51304f));
            StartToVann.Add(new Vector3(-252.1319f, 634.0935f, 34.28012f));
            StartToVann.Add(new Vector3(-248.7516f, 633.8278f, 34.93285f));
            StartToVann.Add(new Vector3(-244.9829f, 633.3923f, 35.58935f));
            StartToVann.Add(new Vector3(-241.4152f, 632.8303f, 36.17918f));
            StartToVann.Add(new Vector3(-237.6829f, 632.1335f, 36.72247f));
            StartToVann.Add(new Vector3(-233.7775f, 631.3582f, 37.24923f));
            StartToVann.Add(new Vector3(-230.1196f, 630.6306f, 37.6594f));
            StartToVann.Add(new Vector3(-226.4616f, 629.903f, 38.05141f));
            StartToVann.Add(new Vector3(-223.0224f, 629.1202f, 38.33427f));
            StartToVann.Add(new Vector3(-219.256f, 628.1846f, 38.62158f));
            StartToVann.Add(new Vector3(-215.8158f, 627.3301f, 38.80013f));
            StartToVann.Add(new Vector3(-212.4734f, 626.4998f, 38.95285f));
            StartToVann.Add(new Vector3(-208.761f, 625.709f, 39.04245f));
            StartToVann.Add(new Vector3(-205.1178f, 625.0895f, 39.09619f));
            StartToVann.Add(new Vector3(-201.6084f, 624.59f, 39.07341f));
            StartToVann.Add(new Vector3(-197.8661f, 624.0574f, 39.01408f));
            StartToVann.Add(new Vector3(-194.0905f, 623.5201f, 38.87204f));
            StartToVann.Add(new Vector3(-190.1986f, 622.9662f, 38.6804f));
            StartToVann.Add(new Vector3(-186.7557f, 622.4762f, 38.44939f));
            StartToVann.Add(new Vector3(-182.9967f, 621.9412f, 38.1554f));
            StartToVann.Add(new Vector3(-179.4041f, 621.4299f, 37.78924f));
            StartToVann.Add(new Vector3(-176.2606f, 620.9825f, 37.44553f));
            StartToVann.Add(new Vector3(-172.4057f, 620.5372f, 36.94715f));
            StartToVann.Add(new Vector3(-168.9787f, 620.1956f, 36.46594f));
            StartToVann.Add(new Vector3(-165.5344f, 619.8578f, 35.91547f));
            StartToVann.Add(new Vector3(-161.7164f, 619.5521f, 35.25791f));
            StartToVann.Add(new Vector3(-158.6271f, 619.4631f, 34.67681f));
            StartToVann.Add(new Vector3(-154.6484f, 619.5837f, 33.87649f));
            StartToVann.Add(new Vector3(-151.262f, 619.8049f, 33.36843f));
            StartToVann.Add(new Vector3(-147.0378f, 620.0873f, 33.40263f));
            StartToVann.Add(new Vector3(-143.3836f, 620.3316f, 33.45213f));
            StartToVann.Add(new Vector3(-139.5006f, 620.6631f, 33.45321f));
            StartToVann.Add(new Vector3(-135.9844f, 621.108f, 33.46188f));
            StartToVann.Add(new Vector3(-132.6285f, 621.6126f, 33.5275f));
            StartToVann.Add(new Vector3(-128.5769f, 622.2346f, 33.88045f));
            StartToVann.Add(new Vector3(-124.9073f, 622.9888f, 34.47579f));
            StartToVann.Add(new Vector3(-121.3441f, 623.7598f, 35.33043f));
            StartToVann.Add(new Vector3(-117.7585f, 624.5837f, 36.22809f));
            StartToVann.Add(new Vector3(-114.426f, 625.3837f, 37.11071f));
            StartToVann.Add(new Vector3(-110.5218f, 626.321f, 38.11916f));
            StartToVann.Add(new Vector3(-106.9769f, 627.1721f, 38.99523f));
            StartToVann.Add(new Vector3(-103.5821f, 627.9871f, 39.81863f));
            StartToVann.Add(new Vector3(-99.5308f, 628.9598f, 40.60347f));
            StartToVann.Add(new Vector3(-95.83891f, 629.8461f, 41.13966f));
            StartToVann.Add(new Vector3(-92.21505f, 630.7279f, 41.33219f));
            StartToVann.Add(new Vector3(-88.68987f, 631.5889f, 41.4124f));
            StartToVann.Add(new Vector3(-85.11572f, 632.4619f, 41.43359f));
            StartToVann.Add(new Vector3(-81.53622f, 633.3114f, 41.42967f));
            StartToVann.Add(new Vector3(-77.57439f, 633.8167f, 41.54068f));
            StartToVann.Add(new Vector3(-73.69516f, 633.863f, 41.65808f));
            StartToVann.Add(new Vector3(-69.86523f, 633.8029f, 41.61757f));
            StartToVann.Add(new Vector3(-66.01851f, 633.7425f, 41.35632f));
            StartToVann.Add(new Vector3(-62.23898f, 633.6831f, 41.43938f));
            StartToVann.Add(new Vector3(-58.68117f, 633.7719f, 41.89314f));
            StartToVann.Add(new Vector3(-54.53318f, 634.6686f, 42.07512f));
            StartToVann.Add(new Vector3(-51.48312f, 635.7149f, 42.4538f));
            StartToVann.Add(new Vector3(-47.80194f, 637.4328f, 43.42822f));
            StartToVann.Add(new Vector3(-45.18102f, 639.1604f, 44.19583f));
            StartToVann.Add(new Vector3(-41.78276f, 641.9521f, 45.24485f));
            StartToVann.Add(new Vector3(-38.99713f, 645.1561f, 46.18753f));
            StartToVann.Add(new Vector3(-36.96994f, 647.9812f, 46.8926f));
            StartToVann.Add(new Vector3(-35.28759f, 650.8488f, 47.54702f));
            StartToVann.Add(new Vector3(-33.45155f, 654.5498f, 48.53263f));
            StartToVann.Add(new Vector3(-31.95835f, 658.1135f, 49.38583f));
            StartToVann.Add(new Vector3(-30.55681f, 661.4972f, 50.02746f));
            StartToVann.Add(new Vector3(-29.00741f, 665.2378f, 50.61978f));
            StartToVann.Add(new Vector3(-27.40657f, 669.1025f, 50.61978f));
            StartToVann.Add(new Vector3(-25.95361f, 672.6104f, 50.61978f));
            StartToVann.Add(new Vector3(-24.51993f, 676.0715f, 50.61978f));
            StartToVann.Add(new Vector3(-23.06696f, 679.5793f, 50.61978f));
            StartToVann.Add(new Vector3(-21.65899f, 682.9785f, 50.61978f));
            StartToVann.Add(new Vector3(-20.25746f, 686.3621f, 50.61978f));
            StartToVann.Add(new Vector3(-18.54561f, 690.0668f, 50.61978f));
            StartToVann.Add(new Vector3(-16.08614f, 692.181f, 50.61978f));
            StartToVann.Add(new Vector3(-12.87064f, 692.9162f, 50.61978f));
            StartToVann.Add(new Vector3(-9.9638f, 692.3679f, 50.61978f));
            StartToVann.Add(new Vector3(-7.078581f, 691.6616f, 50.61978f));
            StartToVann.Add(new Vector3(-4.225273f, 692.3074f, 50.61978f));
            StartToVann.Add(new Vector3(-1.451499f, 693.9674f, 50.61978f));
            StartToVann.Add(new Vector3(0.1404919f, 696.7892f, 50.61978f));
            StartToVann.Add(new Vector3(0.2608401f, 700.2015f, 50.61978f));
            StartToVann.Add(new Vector3(-1.083343f, 703.0179f, 50.61978f));
            StartToVann.Add(new Vector3(-3.064195f, 706.4715f, 50.61978f));
            StartToVann.Add(new Vector3(-4.823432f, 709.5872f, 50.13535f));
            StartToVann.Add(new Vector3(-6.393013f, 713.079f, 50.13535f));
            StartToVann.Add(new Vector3(-7.086874f, 715.2088f, 50.13535f));
            StartToVann.Add(new Vector3(-7.354768f, 717.0955f, 50.13535f));
            StartToVann.Add(new Vector3(-7.538103f, 718.8568f, 50.36884f));
            StartToVann.Add(new Vector3(-7.857282f, 720.5413f, 50.62132f));
            StartToVann.Add(new Vector3(-8.220069f, 722.3651f, 50.62132f));
            StartToVann.Add(new Vector3(-8.570292f, 724.1257f, 50.62132f));
            StartToVann.Add(new Vector3(-8.955066f, 726.0601f, 50.62132f));
            StartToVann.Add(new Vector3(-9.322565f, 727.9076f, 50.62132f));
            StartToVann.Add(new Vector3(-9.754454f, 730.0788f, 50.62132f), WaypointAction.Stop);

            VannsNoAFK.Add(new Vector3(-10.19539f, 726.9243f, 50.62133f),WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-9.686164f, 725.5206f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-8.4035f, 724.4941f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-6.607768f, 724.0939f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-4.924038f, 723.8956f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-3.473414f, 723.0722f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-2.617766f, 721.7197f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-1.866451f, 720.2594f, 50.62133f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-1.534782f, 718.7738f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-1.8642f, 717.1417f, 50.36882f));
            VannsNoAFK.Add(new Vector3(-2.57934f, 715.8716f, 50.36882f));
            VannsNoAFK.Add(new Vector3(-4.008587f, 714.7811f, 50.13536f));
            VannsNoAFK.Add(new Vector3(-5.682908f, 714.3998f, 50.13536f));
            VannsNoAFK.Add(new Vector3(-7.509346f, 714.3703f, 50.13536f));
            VannsNoAFK.Add(new Vector3(-9.357612f, 714.7562f, 50.13536f));
            VannsNoAFK.Add(new Vector3(-11.20467f, 715.6881f, 50.13536f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-12.76539f, 716.9089f, 50.13536f));
            VannsNoAFK.Add(new Vector3(-13.62249f, 718.2539f, 50.13536f));
            VannsNoAFK.Add(new Vector3(-14.16272f, 720.3742f, 50.36887f));
            VannsNoAFK.Add(new Vector3(-14.47967f, 722.3857f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-14.74173f, 724.0307f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-15.4961f, 725.7491f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-16.85586f, 727.0255f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-18.52183f, 727.1327f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-20.33838f, 727.0327f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-22.00605f, 726.5873f, 50.62135f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-23.26499f, 725.4139f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-23.68912f, 723.7305f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-23.35971f, 721.9127f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-22.9567f, 720.2047f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-22.59395f, 719.1153f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-21.09765f, 717.4928f, 50.13541f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-19.50244f, 716.7932f, 50.13541f));
            VannsNoAFK.Add(new Vector3(-17.65629f, 716.6249f, 50.13541f));
            VannsNoAFK.Add(new Vector3(-15.91066f, 717.056f, 50.13541f));
            VannsNoAFK.Add(new Vector3(-14.24109f, 718.1488f, 50.13541f));
            VannsNoAFK.Add(new Vector3(-12.80009f, 719.7595f, 50.13541f));
            VannsNoAFK.Add(new Vector3(-11.76423f, 721.2452f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-10.67684f, 722.8325f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-9.644718f, 724.2101f, 50.62148f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-8.260431f, 725.4489f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-6.524185f, 726.1604f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-3.203924f, 726.1159f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-1.566283f, 725.2464f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-0.3494457f, 724.0229f, 50.62148f));
            VannsNoAFK.Add(new Vector3(0.6055696f, 722.8593f, 50.62148f));
            VannsNoAFK.Add(new Vector3(1.690343f, 721.3917f, 50.62148f));
            VannsNoAFK.Add(new Vector3(2.194803f, 719.8622f, 50.62148f));
            VannsNoAFK.Add(new Vector3(2.168199f, 718.184f, 50.62148f));
            VannsNoAFK.Add(new Vector3(1.667629f, 716.3515f, 50.62148f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(0.435632f, 715.2571f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-0.9364266f, 714.8486f, 50.62148f));
            VannsNoAFK.Add(new Vector3(-2.838761f, 714.3281f, 50.36885f));
            VannsNoAFK.Add(new Vector3(-4.71804f, 714.0932f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-6.208562f, 714.6805f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-7.724929f, 715.7941f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-8.715857f, 716.7916f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-9.51134f, 718.8286f, 50.1503f));
            VannsNoAFK.Add(new Vector3(-9.586344f, 720.4608f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-9.585874f, 722.3364f, 50.62002f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-10.22039f, 724.2181f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-11.69818f, 725.6293f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-13.31339f, 726.1531f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-14.94689f, 725.7671f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-16.52594f, 724.624f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-16.80846f, 723.0524f, 50.62002f));
            VannsNoAFK.Add(new Vector3(-16.3411f, 721.3441f, 50.36885f));
            VannsNoAFK.Add(new Vector3(-15.69271f, 719.4292f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-14.4535f, 718.176f, 50.13537f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-12.70595f, 717.5178f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-11.01855f, 717.3244f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-9.308221f, 717.3568f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-7.823344f, 718.2335f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-6.755525f, 719.6027f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-6.479774f, 721.3238f, 50.62137f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-7.007537f, 723.0308f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-7.784456f, 724.6415f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-9.369428f, 725.575f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-11.33213f, 725.7336f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-13.02106f, 725.8066f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-14.91145f, 725.7048f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-16.69436f, 725.0013f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-18.21203f, 723.8924f, 50.62137f));
            VannsNoAFK.Add(new Vector3(-19.28641f, 722.5837f, 50.62137f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-19.8866f, 720.7737f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-19.98433f, 718.9973f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-20.08241f, 717.2048f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-20.00329f, 715.3853f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-19.3478f, 713.6151f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-18.53191f, 712.2276f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-17.53265f, 710.6797f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-16.16596f, 709.3088f, 50.13538f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-14.71106f, 708.3949f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-12.98905f, 708.1094f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-11.03304f, 708.0864f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-9.334573f, 708.0707f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-7.422064f, 708.1555f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-5.836961f, 708.3866f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-3.966463f, 709.1724f, 50.36884f));
            VannsNoAFK.Add(new Vector3(-2.661378f, 710.2448f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-1.511835f, 711.5026f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-0.6335543f, 713.1684f, 50.62135f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-0.1165727f, 714.5048f, 50.62135f));
            VannsNoAFK.Add(new Vector3(0.6326548f, 716.4505f, 50.62135f));
            VannsNoAFK.Add(new Vector3(0.9313697f, 718.1119f, 50.62135f));
            VannsNoAFK.Add(new Vector3(0.2755328f, 719.8234f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-0.8239939f, 721.176f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-2.345503f, 722.2787f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-3.865589f, 723.2184f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-5.508049f, 723.8701f, 50.62135f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-7.273411f, 723.3405f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-8.456399f, 721.9442f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-9.306718f, 720.1564f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-10.0719f, 718.4793f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-10.91059f, 716.641f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-11.67577f, 714.9639f, 50.13539f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-12.39723f, 713.4175f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-13.70072f, 712.1809f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-15.35495f, 712.0137f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-16.95544f, 712.8029f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-18.15852f, 714.146f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-19.45828f, 715.5971f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-20.55212f, 717.0919f, 50.13539f));
            VannsNoAFK.Add(new Vector3(-21.201f, 718.73f, 50.16734f));
            VannsNoAFK.Add(new Vector3(-21.6149f, 720.5251f, 50.36886f));
            VannsNoAFK.Add(new Vector3(-21.07442f, 722.216f, 50.36886f));
            VannsNoAFK.Add(new Vector3(-20.12524f, 723.6246f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-18.93463f, 725.3551f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-17.49591f, 726.5688f, 50.62135f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-15.89026f, 727.3673f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-14.14489f, 728.0251f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-12.41672f, 728.0629f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-10.72464f, 727.2039f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-9.34023f, 725.9337f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-8.44975f, 724.2527f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-8.693485f, 722.4052f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-9.177089f, 720.6096f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-9.68258f, 718.8806f, 50.14605f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-10.76026f, 717.3811f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-12.24956f, 716.2657f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-14.10468f, 715.8167f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-16.10931f, 716.0295f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-17.91239f, 716.7595f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-19.56629f, 717.627f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-20.89067f, 719.0302f, 50.1354f));
            VannsNoAFK.Add(new Vector3(-21.2835f, 720.8463f, 50.36884f));
            VannsNoAFK.Add(new Vector3(-20.64041f, 722.7883f, 50.45719f));
            VannsNoAFK.Add(new Vector3(-19.74286f, 724.3337f, 50.62138f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-18.76331f, 725.9496f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-17.24511f, 726.8257f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-15.69549f, 727.0463f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-13.60323f, 726.3653f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-12.14735f, 725.2967f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-10.73721f, 723.9409f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-9.443135f, 722.6968f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-8.102635f, 721.408f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-6.721515f, 720.0801f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-5.418317f, 718.8223f, 50.62138f));
            VannsNoAFK.Add(new Vector3(-4.168498f, 717.4486f, 50.36885f));
            VannsNoAFK.Add(new Vector3(-3.311312f, 715.9848f, 50.13535f));
            VannsNoAFK.Add(new Vector3(-2.69529f, 714.0267f, 50.36887f));
            VannsNoAFK.Add(new Vector3(-2.712917f, 712.1099f, 50.47754f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-3.145707f, 710.6306f, 50.48517f));
            VannsNoAFK.Add(new Vector3(-4.347441f, 709.0753f, 50.36886f));
            VannsNoAFK.Add(new Vector3(-6.057615f, 708.1277f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-7.692945f, 707.2944f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-9.435862f, 706.4063f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-11.07913f, 705.608f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-12.96387f, 705.415f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-14.65343f, 706.4622f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-15.57796f, 707.9429f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-16.43444f, 710.0104f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-17.20205f, 711.8795f, 50.13538f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-17.71733f, 713.6806f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-17.86623f, 715.1211f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-17.69621f, 717.418f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-17.20246f, 719.1677f, 50.13538f));
            VannsNoAFK.Add(new Vector3(-16.42443f, 720.851f, 50.22027f));
            VannsNoAFK.Add(new Vector3(-15.02985f, 722.0646f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-13.29752f, 722.9733f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-11.71723f, 723.7711f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-9.973889f, 724.0551f, 50.62135f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-8.136463f, 723.8713f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-6.440697f, 723.0059f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-4.779629f, 721.988f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-3.198301f, 720.8007f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-2.820853f, 719.2596f, 50.62135f));
            VannsNoAFK.Add(new Vector3(-3.64627f, 716.833f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-4.604135f, 715.6719f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-6.377909f, 713.5559f, 50.13537f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-7.906218f, 712.3973f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-9.513848f, 711.717f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-11.41333f, 711.0323f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-13.52801f, 710.8212f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-15.36898f, 711.1619f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-17.05736f, 712.1638f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-18.76542f, 713.2127f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-19.94964f, 714.3999f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-20.87592f, 716.2711f, 50.13537f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-21.29839f, 717.9537f, 50.13537f));
            VannsNoAFK.Add(new Vector3(-21.30697f, 719.7668f, 50.36885f));
            VannsNoAFK.Add(new Vector3(-20.53123f, 721.4013f, 50.3407f));
            VannsNoAFK.Add(new Vector3(-18.97923f, 722.8158f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-17.87809f, 723.7944f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-16.14769f, 725.2053f, 50.62133f), WaypointAction.Jump);
            VannsNoAFK.Add(new Vector3(-14.32312f, 725.7495f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-12.0367f, 726.0797f, 50.62133f));
            VannsNoAFK.Add(new Vector3(-9.985262f, 726.3474f, 50.62133f));

            RHToVann.Add(new Vector3(38.65018f, 644.7535f, 69.03398f));
            RHToVann.Add(new Vector3(36.82683f, 644.8735f, 68.87698f));
            RHToVann.Add(new Vector3(35.48322f, 644.9134f, 68.78271f));
            RHToVann.Add(new Vector3(33.67234f, 644.8785f, 68.66382f));
            RHToVann.Add(new Vector3(31.99038f, 644.8381f, 68.46472f));
            RHToVann.Add(new Vector3(30.29247f, 644.7913f, 68.27241f));
            RHToVann.Add(new Vector3(28.52214f, 644.7425f, 67.975f));
            RHToVann.Add(new Vector3(26.67135f, 644.6915f, 67.54031f));
            RHToVann.Add(new Vector3(24.93321f, 644.6436f, 67.12225f));
            RHToVann.Add(new Vector3(23.09046f, 644.5929f, 66.44923f));
            RHToVann.Add(new Vector3(21.29599f, 644.5435f, 65.80778f));
            RHToVann.Add(new Vector3(19.56589f, 644.4958f, 65.04935f));
            RHToVann.Add(new Vector3(17.58634f, 644.4412f, 64.14206f));
            RHToVann.Add(new Vector3(15.53437f, 644.3847f, 63.07032f));
            RHToVann.Add(new Vector3(13.83647f, 644.338f, 62.25719f));
            RHToVann.Add(new Vector3(12.05809f, 644.2889f, 61.59523f));
            RHToVann.Add(new Vector3(10.36823f, 644.2424f, 60.76921f));
            RHToVann.Add(new Vector3(8.404776f, 644.1883f, 59.82808f));
            RHToVann.Add(new Vector3(6.49765f, 644.1357f, 58.84705f));
            RHToVann.Add(new Vector3(4.89682f, 644.0768f, 58.06338f));
            RHToVann.Add(new Vector3(3.027237f, 643.9321f, 57.20065f));
            RHToVann.Add(new Vector3(1.014615f, 643.6804f, 56.31301f));
            RHToVann.Add(new Vector3(-0.8978243f, 643.3542f, 55.48089f));
            RHToVann.Add(new Vector3(-2.667426f, 643.0525f, 54.7032f));
            RHToVann.Add(new Vector3(-4.817929f, 642.6857f, 53.78521f));
            RHToVann.Add(new Vector3(-6.95256f, 642.3217f, 52.88749f));
            RHToVann.Add(new Vector3(-9.007837f, 641.9712f, 52.05124f));
            RHToVann.Add(new Vector3(-11.1504f, 641.6058f, 51.23031f));
            RHToVann.Add(new Vector3(-12.6502f, 641.35f, 50.66644f));
            RHToVann.Add(new Vector3(-14.19761f, 641.0861f, 50.10966f));
            RHToVann.Add(new Vector3(-15.70534f, 640.829f, 49.57203f));
            RHToVann.Add(new Vector3(-17.29243f, 640.5583f, 49.04725f));
            RHToVann.Add(new Vector3(-18.72081f, 640.3148f, 48.62563f));
            RHToVann.Add(new Vector3(-20.51422f, 640.0089f, 48.14034f));
            RHToVann.Add(new Vector3(-22.12511f, 639.7342f, 47.7597f));
            RHToVann.Add(new Vector3(-23.83031f, 639.5645f, 47.41035f));
            RHToVann.Add(new Vector3(-24.94685f, 639.6032f, 47.2061f));
            RHToVann.Add(new Vector3(-26.93356f, 640.5352f, 46.90818f));
            RHToVann.Add(new Vector3(-27.34738f, 642.1754f, 46.91635f));
            RHToVann.Add(new Vector3(-27.36463f, 643.6323f, 46.98525f));
            RHToVann.Add(new Vector3(-27.38445f, 645.3066f, 47.10283f));
            RHToVann.Add(new Vector3(-27.39843f, 647.2949f, 47.2659f));
            RHToVann.Add(new Vector3(-27.36856f, 648.993f, 47.45287f));
            RHToVann.Add(new Vector3(-27.30192f, 651.1172f, 47.73997f));
            RHToVann.Add(new Vector3(-27.2373f, 653.177f, 48.25267f));
            RHToVann.Add(new Vector3(-27.17647f, 655.1161f, 48.83736f));
            RHToVann.Add(new Vector3(-27.11412f, 657.1034f, 49.39004f));
            RHToVann.Add(new Vector3(-27.06263f, 658.7448f, 49.74394f));
            RHToVann.Add(new Vector3(-26.99978f, 660.7483f, 50.0782f));
            RHToVann.Add(new Vector3(-26.86551f, 662.3821f, 50.35484f));
            RHToVann.Add(new Vector3(-26.54944f, 664.0756f, 50.62007f));
            RHToVann.Add(new Vector3(-26.24963f, 665.6819f, 50.62007f));
            RHToVann.Add(new Vector3(-25.79769f, 668.1035f, 50.62007f));
            RHToVann.Add(new Vector3(-25.46686f, 669.876f, 50.62007f));
            RHToVann.Add(new Vector3(-25.16557f, 671.4904f, 50.62007f));
            RHToVann.Add(new Vector3(-24.78009f, 673.5557f, 50.62007f));
            RHToVann.Add(new Vector3(-24.4876f, 675.1716f, 50.62007f));
            RHToVann.Add(new Vector3(-24.19007f, 676.9175f, 50.62007f));
            RHToVann.Add(new Vector3(-23.87494f, 678.7665f, 50.62007f));
            RHToVann.Add(new Vector3(-23.55036f, 680.671f, 50.62007f));
            RHToVann.Add(new Vector3(-23.23795f, 682.5042f, 50.62007f));
            RHToVann.Add(new Vector3(-22.90389f, 684.4642f, 50.62007f));
            RHToVann.Add(new Vector3(-22.58201f, 686.3529f, 50.62007f));
            RHToVann.Add(new Vector3(-22.33311f, 688.0891f, 50.62007f));
            RHToVann.Add(new Vector3(-22.32428f, 689.9535f, 50.62007f));
            RHToVann.Add(new Vector3(-22.70655f, 691.619f, 50.62007f));
            RHToVann.Add(new Vector3(-23.4577f, 693.1045f, 50.62007f));
            RHToVann.Add(new Vector3(-24.55733f, 694.3475f, 50.62007f));
            RHToVann.Add(new Vector3(-25.96853f, 695.4697f, 50.62007f));
            RHToVann.Add(new Vector3(-27.57929f, 696.7292f, 50.62007f));
            RHToVann.Add(new Vector3(-28.92371f, 697.7804f, 50.62007f));
            RHToVann.Add(new Vector3(-30.35289f, 699.0297f, 50.62007f));
            RHToVann.Add(new Vector3(-31.56983f, 700.3704f, 50.62007f));
            RHToVann.Add(new Vector3(-32.68018f, 701.7704f, 50.62007f));
            RHToVann.Add(new Vector3(-33.61996f, 703.2397f, 50.62007f));
            RHToVann.Add(new Vector3(-34.23601f, 704.8628f, 50.62007f));
            RHToVann.Add(new Vector3(-34.33866f, 706.2455f, 50.62007f));
            RHToVann.Add(new Vector3(-33.76209f, 708.1696f, 50.62007f));
            RHToVann.Add(new Vector3(-32.86146f, 709.7769f, 50.62007f));
            RHToVann.Add(new Vector3(-31.91489f, 711.2352f, 50.62007f));
            RHToVann.Add(new Vector3(-30.99135f, 712.5439f, 50.62007f));
            RHToVann.Add(new Vector3(-29.92742f, 713.8569f, 50.62007f));
            RHToVann.Add(new Vector3(-28.66109f, 715.1736f, 50.62007f));
            RHToVann.Add(new Vector3(-27.48969f, 716.3359f, 50.62007f));
            RHToVann.Add(new Vector3(-26.28971f, 717.5267f, 50.62007f));
            RHToVann.Add(new Vector3(-24.95283f, 718.8188f, 50.62007f));
            RHToVann.Add(new Vector3(-23.65137f, 719.8975f, 50.62007f));
            RHToVann.Add(new Vector3(-22.1639f, 720.8997f, 50.62007f));
            RHToVann.Add(new Vector3(-20.57843f, 721.8715f, 50.36883f));
            RHToVann.Add(new Vector3(-18.66352f, 723.045f, 50.62134f));
            RHToVann.Add(new Vector3(-17.11236f, 723.9957f, 50.62134f));
            RHToVann.Add(new Vector3(-15.5818f, 724.9338f, 50.62134f));
            RHToVann.Add(new Vector3(-13.99633f, 725.9055f, 50.62134f));
            RHToVann.Add(new Vector3(-12.35333f, 726.8901f, 50.62134f));
            RHToVann.Add(new Vector3(-10.61886f, 727.4813f, 50.62134f));
            RHToVann.Add(new Vector3(-8.688756f, 727.6582f, 50.62134f), WaypointAction.Stop);


            TopGYToVann.Add(new Vector3(-364.0392f, 670.0502f, 29.70333f));
            TopGYToVann.Add(new Vector3(-361.7625f, 669.1837f, 29.54127f));
            TopGYToVann.Add(new Vector3(-358.4652f, 667.9289f, 29.35156f));
            TopGYToVann.Add(new Vector3(-355.6389f, 666.8533f, 29.12436f));
            TopGYToVann.Add(new Vector3(-353.3695f, 666.071f, 29.18079f));
            TopGYToVann.Add(new Vector3(-350.0178f, 665.7466f, 29.40871f));
            TopGYToVann.Add(new Vector3(-347.3423f, 665.7286f, 29.54596f));
            TopGYToVann.Add(new Vector3(-344.6208f, 665.7115f, 29.63651f));
            TopGYToVann.Add(new Vector3(-341.3112f, 665.6897f, 29.66592f));
            TopGYToVann.Add(new Vector3(-338.2321f, 665.5812f, 29.77603f));
            TopGYToVann.Add(new Vector3(-335.3845f, 665.3649f, 29.97008f));
            TopGYToVann.Add(new Vector3(-332.192f, 665.0421f, 30.25579f));
            TopGYToVann.Add(new Vector3(-329.1177f, 664.7197f, 30.77167f));
            TopGYToVann.Add(new Vector3(-326.0099f, 664.3939f, 31.17505f));
            TopGYToVann.Add(new Vector3(-323.2697f, 664.1066f, 31.11073f));
            TopGYToVann.Add(new Vector3(-320.1452f, 663.779f, 30.92606f));
            TopGYToVann.Add(new Vector3(-316.9706f, 663.4462f, 30.88243f));
            TopGYToVann.Add(new Vector3(-314.1474f, 663.1458f, 30.52165f));
            TopGYToVann.Add(new Vector3(-310.8918f, 662.5583f, 30.07516f));
            TopGYToVann.Add(new Vector3(-307.6675f, 661.6204f, 30.00813f));
            TopGYToVann.Add(new Vector3(-304.6708f, 660.4029f, 30.1681f));
            TopGYToVann.Add(new Vector3(-302.0035f, 658.9136f, 30.29097f));
            TopGYToVann.Add(new Vector3(-299.257f, 657.0377f, 30.30736f));
            TopGYToVann.Add(new Vector3(-296.6838f, 655.2354f, 30.29156f));
            TopGYToVann.Add(new Vector3(-294.0144f, 653.3655f, 30.25579f));
            TopGYToVann.Add(new Vector3(-291.3587f, 651.5052f, 30.18458f));
            TopGYToVann.Add(new Vector3(-288.8268f, 649.7318f, 30.28192f));
            TopGYToVann.Add(new Vector3(-286.2537f, 647.9293f, 30.38011f));
            TopGYToVann.Add(new Vector3(-283.3016f, 646.0974f, 30.49898f));
            TopGYToVann.Add(new Vector3(-280.9413f, 644.8469f, 30.55057f));
            TopGYToVann.Add(new Vector3(-278.0073f, 643.3154f, 30.54703f));
            TopGYToVann.Add(new Vector3(-275.2536f, 641.9868f, 30.51599f));
            TopGYToVann.Add(new Vector3(-272.084f, 640.6423f, 30.39151f));
            TopGYToVann.Add(new Vector3(-269.5128f, 639.6522f, 30.07868f));
            TopGYToVann.Add(new Vector3(-266.1736f, 638.5174f, 30.98576f));
            TopGYToVann.Add(new Vector3(-263.3258f, 637.7225f, 31.72508f));
            TopGYToVann.Add(new Vector3(-260.0709f, 636.8889f, 32.51863f));
            TopGYToVann.Add(new Vector3(-257.1287f, 636.2794f, 33.18679f));
            TopGYToVann.Add(new Vector3(-254.1461f, 635.8954f, 33.84434f));
            TopGYToVann.Add(new Vector3(-251.1468f, 635.5094f, 34.42777f));
            TopGYToVann.Add(new Vector3(-247.781f, 635.0761f, 35.0825f));
            TopGYToVann.Add(new Vector3(-245.065f, 634.7265f, 35.54397f));
            TopGYToVann.Add(new Vector3(-242.3823f, 634.3812f, 35.98562f));
            TopGYToVann.Add(new Vector3(-238.5999f, 633.8942f, 36.56601f));
            TopGYToVann.Add(new Vector3(-235.6174f, 633.5103f, 36.96895f));
            TopGYToVann.Add(new Vector3(-233.4179f, 633.2272f, 37.25889f));
            TopGYToVann.Add(new Vector3(-229.4356f, 632.7145f, 37.69883f));
            TopGYToVann.Add(new Vector3(-226.1864f, 632.2963f, 38.04292f));
            TopGYToVann.Add(new Vector3(-222.9039f, 631.8737f, 38.31266f));
            TopGYToVann.Add(new Vector3(-220.0379f, 631.5048f, 38.52751f));
            TopGYToVann.Add(new Vector3(-216.7736f, 631.073f, 38.73325f));
            TopGYToVann.Add(new Vector3(-214.0257f, 630.7083f, 38.85671f));
            TopGYToVann.Add(new Vector3(-210.5862f, 630.1982f, 39.00683f));
            TopGYToVann.Add(new Vector3(-207.445f, 629.5433f, 39.05338f));
            TopGYToVann.Add(new Vector3(-204.6305f, 628.9681f, 39.09509f));
            TopGYToVann.Add(new Vector3(-201.4375f, 628.4041f, 39.07957f));
            TopGYToVann.Add(new Vector3(-198.5042f, 627.9168f, 39.03303f));
            TopGYToVann.Add(new Vector3(-195.618f, 627.4527f, 38.96928f));
            TopGYToVann.Add(new Vector3(-192.7485f, 626.9913f, 38.83398f));
            TopGYToVann.Add(new Vector3(-189.8292f, 626.5218f, 38.68816f));
            TopGYToVann.Add(new Vector3(-186.9431f, 626.0577f, 38.50466f));
            TopGYToVann.Add(new Vector3(-183.692f, 625.5349f, 38.24968f));
            TopGYToVann.Add(new Vector3(-180.7728f, 625.0655f, 37.99387f));
            TopGYToVann.Add(new Vector3(-177.8535f, 624.596f, 37.67382f));
            TopGYToVann.Add(new Vector3(-174.5361f, 624.0626f, 37.31028f));
            TopGYToVann.Add(new Vector3(-171.5455f, 623.6161f, 36.89174f));
            TopGYToVann.Add(new Vector3(-168.5433f, 623.2536f, 36.46881f));
            TopGYToVann.Add(new Vector3(-165.9076f, 622.9386f, 36.05727f));
            TopGYToVann.Add(new Vector3(-162.4838f, 622.5671f, 35.46522f));
            TopGYToVann.Add(new Vector3(-159.8438f, 622.2903f, 35.00636f));
            TopGYToVann.Add(new Vector3(-156.8182f, 621.9885f, 34.38734f));
            TopGYToVann.Add(new Vector3(-153.8304f, 621.8647f, 33.78001f));
            TopGYToVann.Add(new Vector3(-150.573f, 621.897f, 33.36841f));
            TopGYToVann.Add(new Vector3(-147.4249f, 622.1525f, 33.38778f));
            TopGYToVann.Add(new Vector3(-144.3781f, 622.4091f, 33.54259f));
            TopGYToVann.Add(new Vector3(-141.2644f, 622.6714f, 33.71169f));
            TopGYToVann.Add(new Vector3(-138.1841f, 622.9308f, 33.74511f));
            TopGYToVann.Add(new Vector3(-134.8024f, 623.2157f, 33.69073f));
            TopGYToVann.Add(new Vector3(-131.471f, 623.4963f, 33.76857f));
            TopGYToVann.Add(new Vector3(-128.6756f, 623.7343f, 34.02806f));
            TopGYToVann.Add(new Vector3(-125.6711f, 624.0745f, 34.35302f));
            TopGYToVann.Add(new Vector3(-123.2952f, 624.4308f, 34.8978f));
            TopGYToVann.Add(new Vector3(-119.8088f, 625.063f, 35.79727f));
            TopGYToVann.Add(new Vector3(-116.8664f, 625.7607f, 36.63367f));
            TopGYToVann.Add(new Vector3(-113.9142f, 626.4158f, 37.4287f));
            TopGYToVann.Add(new Vector3(-111.4208f, 626.967f, 38.02483f));
            TopGYToVann.Add(new Vector3(-108.3368f, 627.6487f, 38.7666f));
            TopGYToVann.Add(new Vector3(-105.7735f, 628.1942f, 39.35128f));
            TopGYToVann.Add(new Vector3(-103.1662f, 628.6918f, 39.94291f));
            TopGYToVann.Add(new Vector3(-100.1311f, 629.1797f, 40.527f));
            TopGYToVann.Add(new Vector3(-97.41763f, 629.5485f, 40.93323f));
            TopGYToVann.Add(new Vector3(-94.77076f, 629.9083f, 41.18115f));
            TopGYToVann.Add(new Vector3(-91.75767f, 630.3177f, 41.33607f));
            TopGYToVann.Add(new Vector3(-89.59356f, 630.6119f, 41.38623f));
            TopGYToVann.Add(new Vector3(-85.96452f, 631.1051f, 41.41564f));
            TopGYToVann.Add(new Vector3(-82.80161f, 631.535f, 41.40387f));
            TopGYToVann.Add(new Vector3(-79.67197f, 631.9603f, 41.40679f));
            TopGYToVann.Add(new Vector3(-76.559f, 632.3834f, 41.51895f));
            TopGYToVann.Add(new Vector3(-73.52822f, 632.7875f, 41.58846f));
            TopGYToVann.Add(new Vector3(-70.42801f, 633.1786f, 41.60933f));
            TopGYToVann.Add(new Vector3(-67.42696f, 633.5505f, 41.40379f));
            TopGYToVann.Add(new Vector3(-64.04246f, 633.97f, 41.40315f));
            TopGYToVann.Add(new Vector3(-60.65827f, 634.3919f, 41.62968f));
            TopGYToVann.Add(new Vector3(-57.62783f, 634.9069f, 41.88923f));
            TopGYToVann.Add(new Vector3(-54.38752f, 635.7172f, 42.19809f));
            TopGYToVann.Add(new Vector3(-51.5822f, 636.9656f, 42.68529f));
            TopGYToVann.Add(new Vector3(-48.74905f, 638.8915f, 43.51148f));
            TopGYToVann.Add(new Vector3(-46.10366f, 641.602f, 44.49442f));
            TopGYToVann.Add(new Vector3(-44.29455f, 644.1909f, 45.51453f));
            TopGYToVann.Add(new Vector3(-42.52194f, 646.7642f, 46.51518f));
            TopGYToVann.Add(new Vector3(-40.51107f, 649.6835f, 47.36104f));
            TopGYToVann.Add(new Vector3(-38.32866f, 652.8517f, 48.12111f));
            TopGYToVann.Add(new Vector3(-36.75618f, 655.1346f, 48.61203f));
            TopGYToVann.Add(new Vector3(-34.81421f, 657.9584f, 49.21907f));
            TopGYToVann.Add(new Vector3(-33.01764f, 661.2029f, 49.85806f));
            TopGYToVann.Add(new Vector3(-31.99563f, 663.8686f, 50.34883f));
            TopGYToVann.Add(new Vector3(-30.88177f, 667.0564f, 50.61966f));
            TopGYToVann.Add(new Vector3(-29.80669f, 670.1332f, 50.61966f));
            TopGYToVann.Add(new Vector3(-28.5926f, 673.4272f, 50.61966f));
            TopGYToVann.Add(new Vector3(-27.28399f, 676.6674f, 50.61966f));
            TopGYToVann.Add(new Vector3(-25.6815f, 679.8831f, 50.61966f));
            TopGYToVann.Add(new Vector3(-23.93317f, 682.8892f, 50.61966f));
            TopGYToVann.Add(new Vector3(-21.90462f, 685.7828f, 50.61966f));
            TopGYToVann.Add(new Vector3(-19.42878f, 688.3856f, 50.61966f));
            TopGYToVann.Add(new Vector3(-16.53503f, 690.2519f, 50.61966f));
            TopGYToVann.Add(new Vector3(-13.22269f, 691.1576f, 50.6197f));
            TopGYToVann.Add(new Vector3(-10.35451f, 691.5084f, 50.61979f));
            TopGYToVann.Add(new Vector3(-7.001744f, 691.9105f, 50.61992f));
            TopGYToVann.Add(new Vector3(-4.073189f, 692.6662f, 50.61992f));
            TopGYToVann.Add(new Vector3(-1.190946f, 694.4421f, 50.61992f));
            TopGYToVann.Add(new Vector3(0.6395909f, 696.8347f, 50.61992f));
            TopGYToVann.Add(new Vector3(0.9705359f, 699.9778f, 50.61992f));
            TopGYToVann.Add(new Vector3(0.003746732f, 702.4134f, 50.61992f));
            TopGYToVann.Add(new Vector3(-0.6987788f, 703.9155f, 50.61992f));
            TopGYToVann.Add(new Vector3(-1.397894f, 705.4104f, 50.61992f));
            TopGYToVann.Add(new Vector3(-2.032576f, 706.7576f, 50.61992f));
            TopGYToVann.Add(new Vector3(-2.67406f, 708.1016f, 50.61992f));
            TopGYToVann.Add(new Vector3(-3.412634f, 709.649f, 50.50861f));
            TopGYToVann.Add(new Vector3(-4.061053f, 711.0076f, 50.36886f));
            TopGYToVann.Add(new Vector3(-4.685642f, 712.3417f, 50.13538f));
            TopGYToVann.Add(new Vector3(-5.254561f, 713.718f, 50.13538f));
            TopGYToVann.Add(new Vector3(-5.658854f, 714.9659f, 50.13538f));
            TopGYToVann.Add(new Vector3(-6.13581f, 716.3596f, 50.13538f));
            TopGYToVann.Add(new Vector3(-6.725954f, 717.6901f, 50.13538f));
            TopGYToVann.Add(new Vector3(-7.380737f, 718.9827f, 50.43488f));
            TopGYToVann.Add(new Vector3(-8.060985f, 720.3256f, 50.62137f));
            TopGYToVann.Add(new Vector3(-8.77761f, 721.7403f, 50.62137f));

            StartToEnd.Add(new Vector3(-495.655f, 866.2516f, 96.56923f));
            StartToEnd.Add(new Vector3(-495.8627f, 865.7364f, 96.53633f));
            StartToEnd.Add(new Vector3(-495.8627f, 865.7364f, 96.53633f));
            StartToEnd.Add(new Vector3(-497.3799f, 861.9736f, 96.29652f));
            StartToEnd.Add(new Vector3(-498.0588f, 860.2898f, 96.30548f));
            StartToEnd.Add(new Vector3(-498.7219f, 858.3569f, 96.29496f));
            StartToEnd.Add(new Vector3(-499.2188f, 856.507f, 96.30514f));
            StartToEnd.Add(new Vector3(-499.5422f, 854.6817f, 96.35856f));
            StartToEnd.Add(new Vector3(-499.689f, 852.7959f, 96.4285f));
            StartToEnd.Add(new Vector3(-499.734f, 850.8164f, 96.70332f));
            StartToEnd.Add(new Vector3(-499.6999f, 848.6354f, 97.16018f));
            StartToEnd.Add(new Vector3(-499.6181f, 846.4232f, 97.6616f));
            StartToEnd.Add(new Vector3(-499.5124f, 844.5023f, 98.10492f));
            StartToEnd.Add(new Vector3(-499.3335f, 842.4815f, 98.22086f));
            StartToEnd.Add(new Vector3(-499.1451f, 840.4374f, 98.46397f));
            StartToEnd.Add(new Vector3(-498.9707f, 838.5457f, 98.68897f));
            StartToEnd.Add(new Vector3(-498.7927f, 836.6138f, 98.91872f));
            StartToEnd.Add(new Vector3(-498.5881f, 834.6846f, 99.18705f));
            StartToEnd.Add(new Vector3(-498.3703f, 832.7405f, 99.49003f));
            StartToEnd.Add(new Vector3(-498.128f, 830.6049f, 99.82374f));
            StartToEnd.Add(new Vector3(-497.8953f, 828.7275f, 100.1222f));
            StartToEnd.Add(new Vector3(-497.6418f, 826.6824f, 100.3397f));
            StartToEnd.Add(new Vector3(-497.3942f, 824.6852f, 100.5277f));
            StartToEnd.Add(new Vector3(-497.0962f, 822.2805f, 100.5896f));
            StartToEnd.Add(new Vector3(-496.9149f, 820.8185f, 100.4805f));
            StartToEnd.Add(new Vector3(-496.6615f, 818.7734f, 100.3442f));
            StartToEnd.Add(new Vector3(-496.3842f, 816.5365f, 100.1951f));
            StartToEnd.Add(new Vector3(-496.1733f, 814.8349f, 100.12f));
            StartToEnd.Add(new Vector3(-495.9218f, 812.8057f, 100.1219f));
            StartToEnd.Add(new Vector3(-495.6455f, 810.5768f, 100.1248f));
            StartToEnd.Add(new Vector3(-495.3891f, 808.5077f, 100.1275f));
            StartToEnd.Add(new Vector3(-495.1378f, 806.4704f, 100.0396f));
            StartToEnd.Add(new Vector3(-494.9322f, 804.5737f, 99.92052f));
            StartToEnd.Add(new Vector3(-494.7151f, 802.5648f, 99.79428f));
            StartToEnd.Add(new Vector3(-494.5077f, 800.644f, 99.67359f));
            StartToEnd.Add(new Vector3(-494.2886f, 798.6164f, 99.67582f));
            StartToEnd.Add(new Vector3(-494.0742f, 796.6315f, 99.72242f));
            StartToEnd.Add(new Vector3(-494.0599f, 796.2126f, 99.73144f));
            StartToEnd.Add(new Vector3(-493.8556f, 793.9432f, 99.72131f));
            StartToEnd.Add(new Vector3(-493.681f, 792.3265f, 99.60274f));
            StartToEnd.Add(new Vector3(-493.4985f, 790.6378f, 99.47875f));
            StartToEnd.Add(new Vector3(-493.3351f, 789.1251f, 99.36783f));
            StartToEnd.Add(new Vector3(-493.1804f, 787.6925f, 99.33483f));
            StartToEnd.Add(new Vector3(-493.0801f, 786.7641f, 99.4105f));
            StartToEnd.Add(new Vector3(-492.9072f, 785.1634f, 99.52942f));
            StartToEnd.Add(new Vector3(-492.7446f, 783.6588f, 99.6412f));
            StartToEnd.Add(new Vector3(-492.57f, 782.0421f, 99.7613f));
            StartToEnd.Add(new Vector3(-492.4083f, 780.5454f, 99.87247f));
            StartToEnd.Add(new Vector3(-492.2674f, 779.2409f, 99.93963f));
            StartToEnd.Add(new Vector3(-492.1014f, 777.7042f, 99.572f));
            StartToEnd.Add(new Vector3(-491.9345f, 776.1595f, 99.19023f));
            StartToEnd.Add(new Vector3(-491.7702f, 774.6389f, 98.81435f));
            StartToEnd.Add(new Vector3(-491.0886f, 768.329f, 97.76765f));
            StartToEnd.Add(new Vector3(-490.7386f, 765.0887f, 97.57742f), WaypointAction.Mount);
            StartToEnd.Add(new Vector3(-490.4445f, 762.3661f, 97.32972f));
            StartToEnd.Add(new Vector3(-489.7681f, 758.6115f, 96.22562f));
            StartToEnd.Add(new Vector3(-488.0089f, 754.9465f, 94.16405f));
            StartToEnd.Add(new Vector3(-485.2668f, 749.2932f, 90.70983f));
            StartToEnd.Add(new Vector3(-484.0717f, 746.8293f, 88.85954f));
            StartToEnd.Add(new Vector3(-482.7227f, 744.048f, 87.13919f));
            StartToEnd.Add(new Vector3(-481.271f, 741.0551f, 85.56348f));
            StartToEnd.Add(new Vector3(-479.6653f, 737.7448f, 83.88537f));
            StartToEnd.Add(new Vector3(-477.9276f, 734.1624f, 82.36403f));
            StartToEnd.Add(new Vector3(-476.014f, 730.2172f, 80.74014f));
            StartToEnd.Add(new Vector3(-474.2617f, 726.6046f, 79.24931f));
            StartToEnd.Add(new Vector3(-472.5876f, 723.2155f, 77.66579f));
            StartToEnd.Add(new Vector3(-470.8939f, 719.95f, 75.71941f));
            StartToEnd.Add(new Vector3(-468.7919f, 716.6318f, 73.45341f));
            StartToEnd.Add(new Vector3(-466.3607f, 714.0559f, 71.53522f));
            StartToEnd.Add(new Vector3(-463.6273f, 711.8853f, 69.50543f));
            StartToEnd.Add(new Vector3(-460.5354f, 710.0847f, 67.61692f));
            StartToEnd.Add(new Vector3(-457.4563f, 708.3284f, 65.91611f));
            StartToEnd.Add(new Vector3(-454.0123f, 706.3641f, 64.60644f));
            StartToEnd.Add(new Vector3(-450.5975f, 704.4164f, 63.57163f));
            StartToEnd.Add(new Vector3(-447.1948f, 702.5157f, 63.02152f));
            StartToEnd.Add(new Vector3(-443.6251f, 700.535f, 62.6818f));
            StartToEnd.Add(new Vector3(-440.5875f, 698.8419f, 62.72982f));
            StartToEnd.Add(new Vector3(-437.5454f, 697.1226f, 62.74556f));
            StartToEnd.Add(new Vector3(-434.084f, 695.088f, 62.84656f));
            StartToEnd.Add(new Vector3(-431.0768f, 693.2122f, 63.08574f));
            StartToEnd.Add(new Vector3(-427.8835f, 690.7257f, 63.57312f));
            StartToEnd.Add(new Vector3(-425.1704f, 688.2181f, 64.20335f));
            StartToEnd.Add(new Vector3(-422.8234f, 685.6532f, 64.60114f));
            StartToEnd.Add(new Vector3(-420.2748f, 682.5264f, 64.94732f));
            StartToEnd.Add(new Vector3(-418.1608f, 679.5776f, 65.48409f));
            StartToEnd.Add(new Vector3(-416.1283f, 676.612f, 65.96781f));
            StartToEnd.Add(new Vector3(-414.1556f, 673.6873f, 66.48337f));
            StartToEnd.Add(new Vector3(-412.1842f, 670.5808f, 66.87192f));
            StartToEnd.Add(new Vector3(-410.1767f, 667.4177f, 67.18703f));
            StartToEnd.Add(new Vector3(-408.3403f, 664.524f, 67.41481f));
            StartToEnd.Add(new Vector3(-406.2878f, 661.2899f, 67.69435f));
            StartToEnd.Add(new Vector3(-403.9383f, 657.5877f, 67.75231f));
            StartToEnd.Add(new Vector3(-402.1199f, 654.7224f, 67.92603f));
            StartToEnd.Add(new Vector3(-399.9414f, 651.2898f, 67.98004f));
            StartToEnd.Add(new Vector3(-397.8979f, 648.0698f, 68.04749f));
            StartToEnd.Add(new Vector3(-395.8635f, 644.8641f, 68.0125f));
            StartToEnd.Add(new Vector3(-393.937f, 641.8286f, 68.0554f));
            StartToEnd.Add(new Vector3(-391.9465f, 638.3245f, 67.81714f));
            StartToEnd.Add(new Vector3(-390.146f, 634.7925f, 67.60951f));
            StartToEnd.Add(new Vector3(-388.6873f, 631.489f, 67.60464f));
            StartToEnd.Add(new Vector3(-387.5366f, 628.1033f, 67.38538f));
            StartToEnd.Add(new Vector3(-386.8834f, 625.0144f, 67.30623f));
            StartToEnd.Add(new Vector3(-386.4676f, 621.3069f, 67.35812f));
            StartToEnd.Add(new Vector3(-386.4759f, 617.7321f, 67.1543f));
            StartToEnd.Add(new Vector3(-387.0049f, 613.651f, 66.76158f));
            StartToEnd.Add(new Vector3(-387.4641f, 610.4243f, 66.48143f));
            StartToEnd.Add(new Vector3(-388.059f, 606.7433f, 66.25217f));
            StartToEnd.Add(new Vector3(-388.7603f, 603.2172f, 66.06507f));
            StartToEnd.Add(new Vector3(-389.4155f, 599.9903f, 65.9948f));
            StartToEnd.Add(new Vector3(-390.1707f, 596.5441f, 66.10155f));
            StartToEnd.Add(new Vector3(-390.9518f, 593.0267f, 66.17458f));
            StartToEnd.Add(new Vector3(-391.7715f, 589.0446f, 66.0377f));
            StartToEnd.Add(new Vector3(-392.5769f, 585.0767f, 65.56937f));
            StartToEnd.Add(new Vector3(-393.3155f, 581.4381f, 64.91226f));
            StartToEnd.Add(new Vector3(-394.0741f, 577.7007f, 64.16141f));
            StartToEnd.Add(new Vector3(-394.8328f, 573.9634f, 63.21315f));
            StartToEnd.Add(new Vector3(-395.5546f, 570.4071f, 62.02634f));
            StartToEnd.Add(new Vector3(-396.3267f, 566.6038f, 60.66741f));
            StartToEnd.Add(new Vector3(-397.0118f, 563.2287f, 59.22805f));
            StartToEnd.Add(new Vector3(-397.8406f, 559.1456f, 57.4967f));
            StartToEnd.Add(new Vector3(-398.5525f, 555.6387f, 55.99598f));
            StartToEnd.Add(new Vector3(-399.5093f, 551.7927f, 54.39691f));
            StartToEnd.Add(new Vector3(-400.5726f, 548.5527f, 53.11898f));
            StartToEnd.Add(new Vector3(-401.9231f, 544.8608f, 51.93654f));
            StartToEnd.Add(new Vector3(-403.2644f, 541.3272f, 50.88574f));
            StartToEnd.Add(new Vector3(-404.6511f, 538.0831f, 49.75108f));
            StartToEnd.Add(new Vector3(-406.1188f, 534.6544f, 48.99599f));
            StartToEnd.Add(new Vector3(-407.6394f, 531.1022f, 47.87048f));
            StartToEnd.Add(new Vector3(-409.0344f, 527.8434f, 46.695f));
            StartToEnd.Add(new Vector3(-410.6145f, 524.1522f, 45.54927f));
            StartToEnd.Add(new Vector3(-412.1351f, 520.6f, 44.39685f));
            StartToEnd.Add(new Vector3(-413.5896f, 517.2023f, 43.46744f));
            StartToEnd.Add(new Vector3(-415.0904f, 513.6964f, 42.41211f));
            StartToEnd.Add(new Vector3(-416.5052f, 510.3912f, 41.35685f));
            StartToEnd.Add(new Vector3(-418.0853f, 506.7f, 40.26418f));
            StartToEnd.Add(new Vector3(-419.758f, 502.7926f, 39.19471f));
            StartToEnd.Add(new Vector3(-421.2191f, 499.3794f, 38.37672f));
            StartToEnd.Add(new Vector3(-422.8124f, 495.6573f, 37.78297f));
            StartToEnd.Add(new Vector3(-424.3066f, 492.1668f, 37.36126f));
            StartToEnd.Add(new Vector3(-425.8404f, 488.5837f, 37.14734f));
            StartToEnd.Add(new Vector3(-427.2754f, 484.8896f, 36.71861f));
            StartToEnd.Add(new Vector3(-428.3123f, 481.5529f, 36.48748f));
            StartToEnd.Add(new Vector3(-429.1887f, 477.9809f, 36.15405f));
            StartToEnd.Add(new Vector3(-429.8542f, 474.1068f, 35.49509f));
            StartToEnd.Add(new Vector3(-430.2957f, 470.3533f, 34.61283f));
            StartToEnd.Add(new Vector3(-430.4893f, 466.4108f, 33.52674f));
            StartToEnd.Add(new Vector3(-430.4092f, 462.7834f, 32.63381f));
            StartToEnd.Add(new Vector3(-430.1259f, 459.0147f, 31.82049f));
            StartToEnd.Add(new Vector3(-429.5051f, 455.1341f, 31.06026f));
            StartToEnd.Add(new Vector3(-428.6258f, 451.4759f, 29.98644f));
            StartToEnd.Add(new Vector3(-427.4951f, 447.9763f, 28.48146f));
            StartToEnd.Add(new Vector3(-425.8312f, 444.3042f, 26.27492f));
            StartToEnd.Add(new Vector3(-424.3474f, 441.122f, 24.13257f));
            StartToEnd.Add(new Vector3(-422.4932f, 437.5612f, 21.88702f));
            StartToEnd.Add(new Vector3(-420.6149f, 434.3005f, 19.58822f));
            StartToEnd.Add(new Vector3(-418.6718f, 431.0582f, 17.28817f));
            StartToEnd.Add(new Vector3(-416.6441f, 427.6903f, 15.15131f));
            StartToEnd.Add(new Vector3(-414.8243f, 424.6678f, 13.70927f));
            StartToEnd.Add(new Vector3(-412.7187f, 421.1704f, 11.89944f));
            StartToEnd.Add(new Vector3(-410.743f, 417.8889f, 9.758133f));
            StartToEnd.Add(new Vector3(-408.6026f, 414.3338f, 7.02525f));
            StartToEnd.Add(new Vector3(-406.5836f, 410.9803f, 4.773401f));
            StartToEnd.Add(new Vector3(-404.6878f, 407.8271f, 3.684651f));
            StartToEnd.Add(new Vector3(-402.647f, 404.253f, 1.898315f));
            StartToEnd.Add(new Vector3(-400.9219f, 400.5556f, -0.1689779f));
            StartToEnd.Add(new Vector3(-399.7032f, 397.1207f, -0.9102687f));
            StartToEnd.Add(new Vector3(-398.4198f, 392.852f, -0.977782f));
            StartToEnd.Add(new Vector3(-397.5596f, 389.3788f, -1.033793f));
            StartToEnd.Add(new Vector3(-396.739f, 385.6203f, -1.079094f));
            StartToEnd.Add(new Vector3(-395.9957f, 381.808f, -1.0771f));
            StartToEnd.Add(new Vector3(-395.2811f, 378.062f, -0.9716803f));
            StartToEnd.Add(new Vector3(-394.5475f, 374.2169f, -0.6570256f));
            StartToEnd.Add(new Vector3(-393.7888f, 370.2399f, -0.4447032f));
            StartToEnd.Add(new Vector3(-392.9861f, 366.0318f, -0.399897f));
            StartToEnd.Add(new Vector3(-392.2809f, 362.3352f, -0.3542046f));
            StartToEnd.Add(new Vector3(-391.4844f, 358.1601f, -0.5426247f));
            StartToEnd.Add(new Vector3(-390.7571f, 354.3481f, -0.653367f));
            StartToEnd.Add(new Vector3(-390.0236f, 350.503f, -0.4840008f));
            StartToEnd.Add(new Vector3(-389.3185f, 346.8065f, -0.5160863f));
            StartToEnd.Add(new Vector3(-388.6321f, 343.209f, -0.5306204f));
            StartToEnd.Add(new Vector3(-387.8451f, 339.0833f, -0.4817998f));
            StartToEnd.Add(new Vector3(-387.1883f, 335.6404f, -0.5934455f));
            StartToEnd.Add(new Vector3(-386.4736f, 331.8944f, -0.6770551f));
            StartToEnd.Add(new Vector3(-385.8153f, 327.9339f, -0.5466678f));
            StartToEnd.Add(new Vector3(-385.4015f, 324.4308f, -0.6017038f));
            StartToEnd.Add(new Vector3(-385.11f, 320.3931f, -0.7491559f));
            StartToEnd.Add(new Vector3(-384.9956f, 316.4979f, -0.7028847f));
            StartToEnd.Add(new Vector3(-385.1677f, 312.6036f, -0.4786416f));
            StartToEnd.Add(new Vector3(-385.5204f, 309.1257f, -0.1983816f));
            StartToEnd.Add(new Vector3(-386.1497f, 304.4813f, 0.5380692f));
            StartToEnd.Add(new Vector3(-386.752f, 300.9711f, 1.539298f));
            StartToEnd.Add(new Vector3(-387.5551f, 297.0201f, 3.072629f));
            StartToEnd.Add(new Vector3(-388.4792f, 293.3563f, 4.444771f));
            StartToEnd.Add(new Vector3(-389.5827f, 290.3264f, 5.447472f));
            StartToEnd.Add(new Vector3(-391.2796f, 286.5964f, 6.771034f));
            StartToEnd.Add(new Vector3(-392.8817f, 283.6819f, 8.989595f));
            StartToEnd.Add(new Vector3(-394.829f, 280.403f, 10.91673f));
            StartToEnd.Add(new Vector3(-396.8864f, 277.1324f, 13.31832f));
            StartToEnd.Add(new Vector3(-399.0204f, 273.9112f, 15.78602f));
            StartToEnd.Add(new Vector3(-401.1174f, 270.746f, 18.13601f));
            StartToEnd.Add(new Vector3(-403.103f, 267.7488f, 20.55578f));
            StartToEnd.Add(new Vector3(-405.2371f, 264.5276f, 23.4103f));
            StartToEnd.Add(new Vector3(-407.3836f, 261.2744f, 25.93403f));
            StartToEnd.Add(new Vector3(-409.0646f, 258.0807f, 28.23475f));
            StartToEnd.Add(new Vector3(-410.517f, 254.5371f, 30.62127f));
            StartToEnd.Add(new Vector3(-411.6569f, 251.1637f, 32.52169f));
            StartToEnd.Add(new Vector3(-412.7078f, 246.8918f, 34.39132f));
            StartToEnd.Add(new Vector3(-413.3107f, 243.3309f, 35.50779f));
            StartToEnd.Add(new Vector3(-413.8205f, 239.3824f, 36.69347f));
            StartToEnd.Add(new Vector3(-414.0491f, 235.4594f, 37.90311f));
            StartToEnd.Add(new Vector3(-413.9952f, 232.1244f, 38.77198f));
            StartToEnd.Add(new Vector3(-413.7116f, 227.8046f, 39.73103f));
            StartToEnd.Add(new Vector3(-413.3073f, 224.2974f, 40.39911f));
            StartToEnd.Add(new Vector3(-412.5789f, 220.4008f, 41.06948f));
            StartToEnd.Add(new Vector3(-411.782f, 216.6886f, 41.50602f));
            StartToEnd.Add(new Vector3(-410.9825f, 213.1667f, 41.90937f));
            StartToEnd.Add(new Vector3(-409.9673f, 209.2125f, 42.24115f));
            StartToEnd.Add(new Vector3(-408.9456f, 205.5361f, 42.58884f));
            StartToEnd.Add(new Vector3(-407.4207f, 201.6423f, 42.89449f));
            StartToEnd.Add(new Vector3(-405.9266f, 198.1702f, 42.94764f));
            StartToEnd.Add(new Vector3(-404.4096f, 194.8186f, 43.05219f));
            StartToEnd.Add(new Vector3(-402.6731f, 191.3926f, 43.25747f));
            StartToEnd.Add(new Vector3(-400.5333f, 187.3819f, 43.37402f));
            StartToEnd.Add(new Vector3(-398.5642f, 184.1454f, 43.54581f));
            StartToEnd.Add(new Vector3(-396.2629f, 180.7332f, 43.80044f));
            StartToEnd.Add(new Vector3(-394.1233f, 177.6375f, 44.08582f));
            StartToEnd.Add(new Vector3(-391.9131f, 174.6529f, 44.40068f));
            StartToEnd.Add(new Vector3(-389.1761f, 171.4197f, 44.74758f));
            StartToEnd.Add(new Vector3(-386.4818f, 168.557f, 44.99517f));
            StartToEnd.Add(new Vector3(-384.0105f, 165.9459f, 45.09171f));
            StartToEnd.Add(new Vector3(-381.3427f, 163.1274f, 45.30049f));
            StartToEnd.Add(new Vector3(-378.4556f, 160.0771f, 45.78636f));
            StartToEnd.Add(new Vector3(-375.7533f, 157.222f, 46.26766f));
            StartToEnd.Add(new Vector3(-373.1317f, 154.4523f, 46.70584f));
            StartToEnd.Add(new Vector3(-370.1522f, 151.3044f, 46.93087f));
            StartToEnd.Add(new Vector3(-367.3459f, 148.3395f, 46.72575f));
            StartToEnd.Add(new Vector3(-364.6205f, 145.4599f, 46.35395f));
            StartToEnd.Add(new Vector3(-362.0682f, 142.7635f, 46.15557f));
            StartToEnd.Add(new Vector3(-359.0068f, 139.648f, 45.96f));
            StartToEnd.Add(new Vector3(-356.2805f, 136.9576f, 45.88966f));
            StartToEnd.Add(new Vector3(-353.1237f, 133.9387f, 45.551f));
            StartToEnd.Add(new Vector3(-350.2901f, 131.3615f, 44.89953f));
            StartToEnd.Add(new Vector3(-347.397f, 128.7498f, 44.21274f));
            StartToEnd.Add(new Vector3(-344.4042f, 126.0479f, 43.41718f));
            StartToEnd.Add(new Vector3(-341.6108f, 123.5262f, 42.63882f));
            StartToEnd.Add(new Vector3(-338.4559f, 120.6285f, 41.64302f));
            StartToEnd.Add(new Vector3(-335.7162f, 117.9998f, 40.9226f));
            StartToEnd.Add(new Vector3(-332.9066f, 115.2984f, 40.29679f));
            StartToEnd.Add(new Vector3(-329.9153f, 112.4224f, 39.51182f));
            StartToEnd.Add(new Vector3(-327.1542f, 109.7677f, 38.68498f));
            StartToEnd.Add(new Vector3(-324.5504f, 107.2643f, 37.86526f));
            StartToEnd.Add(new Vector3(-321.438f, 104.2718f, 36.83668f));
            StartToEnd.Add(new Vector3(-318.8101f, 101.7451f, 35.95997f));
            StartToEnd.Add(new Vector3(-316.1094f, 99.14857f, 35.05994f));
            StartToEnd.Add(new Vector3(-313.2635f, 96.41229f, 34.11249f));
            StartToEnd.Add(new Vector3(-310.5144f, 93.76917f, 33.19856f));
            StartToEnd.Add(new Vector3(-307.5352f, 90.9048f, 32.21036f));
            StartToEnd.Add(new Vector3(-304.8467f, 88.31989f, 31.32009f));
            StartToEnd.Add(new Vector3(-302.2188f, 85.7932f, 30.45202f));
            StartToEnd.Add(new Vector3(-299.0942f, 82.78912f, 29.41871f));
            StartToEnd.Add(new Vector3(-296.1514f, 79.95968f, 28.44069f));
            StartToEnd.Add(new Vector3(-293.1238f, 77.04874f, 27.43422f));
            StartToEnd.Add(new Vector3(-290.3868f, 74.41725f, 26.52404f));
            StartToEnd.Add(new Vector3(-287.6741f, 71.80906f, 25.62469f));
            StartToEnd.Add(new Vector3(-284.9492f, 69.18921f, 25.45873f));
            StartToEnd.Add(new Vector3(-282.2849f, 66.62759f, 28.17806f));
            StartToEnd.Add(new Vector3(-279.1967f, 63.65844f, 29.55707f));
            StartToEnd.Add(new Vector3(-276.5688f, 61.13174f, 28.11367f));
            StartToEnd.Add(new Vector3(-273.8439f, 58.5119f, 24.42466f));
            StartToEnd.Add(new Vector3(-270.7679f, 55.55439f, 22.79129f));
            StartToEnd.Add(new Vector3(-267.9704f, 52.86469f, 21.421f));
            StartToEnd.Add(new Vector3(-264.8701f, 49.88388f, 18.69987f));
            StartToEnd.Add(new Vector3(-262.1801f, 47.2976f, 17.30436f));
            StartToEnd.Add(new Vector3(-259.5279f, 44.74761f, 16.46959f));
            StartToEnd.Add(new Vector3(-256.5124f, 41.84832f, 15.65876f));
            StartToEnd.Add(new Vector3(-253.9212f, 38.98593f, 14.97546f));
            StartToEnd.Add(new Vector3(-251.5815f, 35.84985f, 14.49459f));
            StartToEnd.Add(new Vector3(-249.5904f, 32.52003f, 14.16235f));
            StartToEnd.Add(new Vector3(-247.6927f, 28.94415f, 14.03453f));
            StartToEnd.Add(new Vector3(-245.8835f, 25.11695f, 13.96529f));
            StartToEnd.Add(new Vector3(-244.3865f, 21.77448f, 14.07962f));
            StartToEnd.Add(new Vector3(-242.7211f, 18.04721f, 13.66287f));
            StartToEnd.Add(new Vector3(-241.2202f, 14.68807f, 13.21286f));
            StartToEnd.Add(new Vector3(-239.8375f, 11.06473f, 12.95335f));
            StartToEnd.Add(new Vector3(-238.7648f, 7.391099f, 12.65877f));
            StartToEnd.Add(new Vector3(-238.066f, 3.438079f, 12.1881f));
            StartToEnd.Add(new Vector3(-237.3892f, -1.132004f, 11.62931f));
            StartToEnd.Add(new Vector3(-237.094f, -4.714794f, 11.26039f));
            StartToEnd.Add(new Vector3(-236.806f, -8.568041f, 10.97736f));
            StartToEnd.Add(new Vector3(-236.5128f, -12.72402f, 10.68502f));
            StartToEnd.Add(new Vector3(-236.3942f, -16.70273f, 10.42975f));
            StartToEnd.Add(new Vector3(-236.5023f, -20.71647f, 10.16387f));
            StartToEnd.Add(new Vector3(-236.6173f, -24.89809f, 9.737371f));
            StartToEnd.Add(new Vector3(-236.7276f, -28.91177f, 10.35215f));
            StartToEnd.Add(new Vector3(-236.8328f, -32.74073f, 10.5749f));
            StartToEnd.Add(new Vector3(-236.9459f, -36.85517f, 10.63453f));
            StartToEnd.Add(new Vector3(-237.0627f, -41.10397f, 10.26869f));
            StartToEnd.Add(new Vector3(-237.1712f, -45.05048f, 10.1166f));
            StartToEnd.Add(new Vector3(-237.2886f, -49.26564f, 10.05659f));
            StartToEnd.Add(new Vector3(-237.4898f, -53.39349f, 9.963999f));
            StartToEnd.Add(new Vector3(-237.7534f, -57.39993f, 9.682263f));
            StartToEnd.Add(new Vector3(-238.0334f, -61.35483f, 9.432564f));
            StartToEnd.Add(new Vector3(-238.2849f, -64.90754f, 9.32371f));
            StartToEnd.Add(new Vector3(-238.5969f, -69.3149f, 9.173092f));
            StartToEnd.Add(new Vector3(-238.8508f, -72.90113f, 8.978428f));
            StartToEnd.Add(new Vector3(-239.1497f, -77.12416f, 8.835374f));
            StartToEnd.Add(new Vector3(-239.4167f, -80.89472f, 8.895972f));
            StartToEnd.Add(new Vector3(-239.7061f, -84.98369f, 9.050518f));
            StartToEnd.Add(new Vector3(-239.9885f, -88.97211f, 9.165084f));
            StartToEnd.Add(new Vector3(-240.2661f, -92.89349f, 9.291182f));
            StartToEnd.Add(new Vector3(-240.539f, -96.74785f, 9.449174f));
            StartToEnd.Add(new Vector3(-240.8451f, -101.0714f, 9.640994f));
            StartToEnd.Add(new Vector3(-241.116f, -104.9091f, 9.790815f));
            StartToEnd.Add(new Vector3(-241.0536f, -108.7213f, 10.02831f));
            StartToEnd.Add(new Vector3(-240.9258f, -112.7849f, 10.10716f));
            StartToEnd.Add(new Vector3(-240.7706f, -117.7216f, 9.831672f));
            StartToEnd.Add(new Vector3(-240.663f, -121.1471f, 9.573899f));
            StartToEnd.Add(new Vector3(-240.5389f, -125.0932f, 9.342438f));
            StartToEnd.Add(new Vector3(-240.3906f, -129.8117f, 9.151321f));
            StartToEnd.Add(new Vector3(-240.2797f, -133.3379f, 8.747185f));
            StartToEnd.Add(new Vector3(-240.1684f, -136.881f, 8.470903f));
            StartToEnd.Add(new Vector3(-239.8426f, -141.7555f, 8.029559f));
            StartToEnd.Add(new Vector3(-239.2058f, -145.5484f, 7.81978f));
            StartToEnd.Add(new Vector3(-238.1655f, -149.8943f, 7.822909f));
            StartToEnd.Add(new Vector3(-236.9653f, -153.7479f, 7.956339f));
            StartToEnd.Add(new Vector3(-235.5213f, -157.656f, 8.435296f));
            StartToEnd.Add(new Vector3(-233.9892f, -161.7105f, 9.463214f));
            StartToEnd.Add(new Vector3(-232.6589f, -165.2308f, 10.1752f));
            StartToEnd.Add(new Vector3(-231.299f, -168.8296f, 10.48092f));
            StartToEnd.Add(new Vector3(-230.0697f, -172.0827f, 10.59973f));
            StartToEnd.Add(new Vector3(-228.5554f, -176.0901f, 10.44703f));
            StartToEnd.Add(new Vector3(-227.237f, -179.5789f, 9.875337f));
            StartToEnd.Add(new Vector3(-225.9009f, -183.1149f, 8.991112f));
            StartToEnd.Add(new Vector3(-224.5825f, -186.6037f, 8.028818f));
            StartToEnd.Add(new Vector3(-223.0919f, -190.5483f, 6.667557f));
            StartToEnd.Add(new Vector3(-221.8092f, -193.9428f, 6.667557f));
            StartToEnd.Add(new Vector3(-220.3068f, -197.9188f, 6.667557f));
            StartToEnd.Add(new Vector3(-218.9409f, -201.5334f, 6.667557f));
            StartToEnd.Add(new Vector3(-217.3613f, -205.7137f, 6.667557f));
            StartToEnd.Add(new Vector3(-215.8648f, -209.6739f, 6.667557f));
            StartToEnd.Add(new Vector3(-214.4989f, -213.2885f, 6.667557f));
            StartToEnd.Add(new Vector3(-212.9905f, -217.2802f, 6.667557f));
            StartToEnd.Add(new Vector3(-211.5178f, -221.1776f, 6.667557f));
            StartToEnd.Add(new Vector3(-210.1875f, -224.6979f, 6.667557f));
            StartToEnd.Add(new Vector3(-208.8989f, -228.1081f, 6.667557f));
            StartToEnd.Add(new Vector3(-207.5568f, -231.6598f, 6.667557f));
            StartToEnd.Add(new Vector3(-205.9118f, -236.0129f, 6.809487f));
            StartToEnd.Add(new Vector3(-204.344f, -240.1618f, 7.123323f));
            StartToEnd.Add(new Vector3(-202.9544f, -243.8392f, 7.415775f));
            StartToEnd.Add(new Vector3(-201.6539f, -247.2809f, 7.629433f));
            StartToEnd.Add(new Vector3(-200.2405f, -251.0211f, 7.755395f));
            StartToEnd.Add(new Vector3(-198.6668f, -255.1857f, 7.584434f));
            StartToEnd.Add(new Vector3(-197.3306f, -258.7217f, 7.257955f));
            StartToEnd.Add(new Vector3(-195.9469f, -262.3834f, 7.101357f));
            StartToEnd.Add(new Vector3(-194.4267f, -266.4065f, 7.09231f));
            StartToEnd.Add(new Vector3(-193.037f, -270.0839f, 7.13661f));
            StartToEnd.Add(new Vector3(-191.739f, -273.7051f, 7.318173f));
            StartToEnd.Add(new Vector3(-190.4259f, -277.7707f, 7.762896f));
            StartToEnd.Add(new Vector3(-189.383f, -281.6979f, 8.186251f));
            StartToEnd.Add(new Vector3(-188.566f, -285.732f, 8.530082f));
            StartToEnd.Add(new Vector3(-187.7214f, -290.086f, 8.809956f));
            StartToEnd.Add(new Vector3(-187.0685f, -294.0816f, 8.980209f));
            StartToEnd.Add(new Vector3(-186.4319f, -298.352f, 9.095752f));
            StartToEnd.Add(new Vector3(-185.7897f, -302.9949f, 9.169175f));
            StartToEnd.Add(new Vector3(-185.3375f, -306.8321f, 9.204779f));
            StartToEnd.Add(new Vector3(-185.0033f, -310.361f, 9.226656f));
            StartToEnd.Add(new Vector3(-184.6002f, -315.014f, 9.24446f));
            StartToEnd.Add(new Vector3(-184.2614f, -318.9812f, 9.253998f));
            StartToEnd.Add(new Vector3(-183.9389f, -323.1182f, 9.345742f));
            StartToEnd.Add(new Vector3(-183.6985f, -326.6993f, 9.419086f));
            StartToEnd.Add(new Vector3(-183.4558f, -330.5557f, 9.484275f));
            StartToEnd.Add(new Vector3(-183.2227f, -334.2611f, 9.529626f));
            StartToEnd.Add(new Vector3(-182.9378f, -338.7882f, 9.42816f));
            StartToEnd.Add(new Vector3(-182.7395f, -342.5795f, 9.362612f));
            StartToEnd.Add(new Vector3(-182.6393f, -346.896f, 9.555042f));
            StartToEnd.Add(new Vector3(-182.9188f, -350.6461f, 10.22639f));
            StartToEnd.Add(new Vector3(-183.2992f, -354.5082f, 10.91421f));
            StartToEnd.Add(new Vector3(-183.719f, -358.7716f, 11.68027f));
            StartToEnd.Add(new Vector3(-184.106f, -362.7006f, 12.2815f));
            StartToEnd.Add(new Vector3(-184.4517f, -366.2116f, 12.69851f));
            StartToEnd.Add(new Vector3(-184.8755f, -370.4745f, 13.06908f));
            StartToEnd.Add(new Vector3(-185.6592f, -374.4799f, 13.36803f));
            StartToEnd.Add(new Vector3(-186.4645f, -378.2935f, 13.6271f));
            StartToEnd.Add(new Vector3(-187.2941f, -382.222f, 14.10191f));
            StartToEnd.Add(new Vector3(-188.0752f, -385.9204f, 14.65289f));
            StartToEnd.Add(new Vector3(-188.936f, -389.9969f, 15.40212f));
            StartToEnd.Add(new Vector3(-189.7379f, -393.794f, 16.57477f));
            StartToEnd.Add(new Vector3(-190.5953f, -397.854f, 18.5471f));
            StartToEnd.Add(new Vector3(-191.3937f, -401.6346f, 20.74023f));
            StartToEnd.Add(new Vector3(-192.3136f, -405.9906f, 22.92292f));
            StartToEnd.Add(new Vector3(-193.0842f, -409.6397f, 24.85516f));
            StartToEnd.Add(new Vector3(-193.9242f, -413.6176f, 26.25229f));
            StartToEnd.Add(new Vector3(-194.6289f, -416.9544f, 26.78465f));
            StartToEnd.Add(new Vector3(-195.4863f, -421.0144f, 26.56552f));
            StartToEnd.Add(new Vector3(-196.0512f, -425.15f, 26.20178f));
            StartToEnd.Add(new Vector3(-196.073f, -428.9954f, 26.66581f));
            StartToEnd.Add(new Vector3(-195.9643f, -432.4544f, 27.84198f));
            StartToEnd.Add(new Vector3(-195.6896f, -436.491f, 29.72302f));
            StartToEnd.Add(new Vector3(-194.9032f, -440.6162f, 32.28724f));
            StartToEnd.Add(new Vector3(-194.0482f, -444.5221f, 35.04248f));
            StartToEnd.Add(new Vector3(-193.4247f, -448.3983f, 37.78199f));
            StartToEnd.Add(new Vector3(-193.144f, -452.5719f, 40.70869f));
            StartToEnd.Add(new Vector3(-193.045f, -456.6022f, 43.72832f));
            StartToEnd.Add(new Vector3(-193.2771f, -460.3412f, 46.82545f));
            StartToEnd.Add(new Vector3(-193.5584f, -464.5486f, 49.59158f));
            StartToEnd.Add(new Vector3(-193.5364f, -468.6254f, 52.20792f));
            StartToEnd.Add(new Vector3(-193.0842f, -472.3944f, 53.97328f));
            StartToEnd.Add(new Vector3(-192.1788f, -476.1109f, 54.88092f));
            StartToEnd.Add(new Vector3(-190.9825f, -479.6966f, 55.18007f));
            StartToEnd.Add(new Vector3(-189.6267f, -483.7603f, 55.97521f));
            StartToEnd.Add(new Vector3(-188.1066f, -487.8535f, 56.68642f));
            StartToEnd.Add(new Vector3(-186.7379f, -491.2324f, 57.08019f));
            StartToEnd.Add(new Vector3(-185.1674f, -495.1096f, 57.37255f));
            StartToEnd.Add(new Vector3(-183.6916f, -498.7533f, 57.46723f));
            StartToEnd.Add(new Vector3(-182.1561f, -502.5721f, 57.77484f));
            StartToEnd.Add(new Vector3(-180.7616f, -506.1936f, 57.94208f));
            StartToEnd.Add(new Vector3(-179.808f, -510.3917f, 57.95291f));
            StartToEnd.Add(new Vector3(-179.9189f, -514.2874f, 57.95783f));
            StartToEnd.Add(new Vector3(-180.4623f, -516.894f, 57.95783f));
            StartToEnd.Add(new Vector3(-181.2731f, -518.8216f, 57.95632f));
            StartToEnd.Add(new Vector3(-182.2475f, -520.424f, 57.95473f));
            StartToEnd.Add(new Vector3(-183.2398f, -521.9776f, 57.95763f));
            StartToEnd.Add(new Vector3(-184.2857f, -523.6115f, 57.95763f));
            StartToEnd.Add(new Vector3(-185.2926f, -525.1844f, 57.95763f));
            StartToEnd.Add(new Vector3(-186.3776f, -526.8795f, 57.95763f));
            StartToEnd.Add(new Vector3(-187.4018f, -528.4795f, 57.9537f));
            StartToEnd.Add(new Vector3(-188.3313f, -530.0792f, 57.94937f));
            StartToEnd.Add(new Vector3(-189.1293f, -531.8987f, 57.95465f));
            StartToEnd.Add(new Vector3(-189.6032f, -533.6702f, 57.95534f));
            StartToEnd.Add(new Vector3(-189.5915f, -535.5383f, 57.95534f));
            StartToEnd.Add(new Vector3(-189.0092f, -537.2158f, 57.95534f));
            StartToEnd.Add(new Vector3(-187.8294f, -538.6516f, 57.95534f));
            StartToEnd.Add(new Vector3(-186.0565f, -539.2279f, 57.95541f));
            StartToEnd.Add(new Vector3(-184.2599f, -539.3778f, 57.95552f));
            StartToEnd.Add(new Vector3(-182.3871f, -539.09f, 57.95552f));
            StartToEnd.Add(new Vector3(-180.8253f, -538.5825f, 57.95552f));
            StartToEnd.Add(new Vector3(-179.0032f, -537.9905f, 57.84713f));
            StartToEnd.Add(new Vector3(-177.1252f, -537.3173f, 57.01243f));
            StartToEnd.Add(new Vector3(-175.5066f, -536.5416f, 57.01243f));
            StartToEnd.Add(new Vector3(-173.8242f, -535.6592f, 57.01243f));
            StartToEnd.Add(new Vector3(-171.9724f, -534.6838f, 57.01243f));
            StartToEnd.Add(new Vector3(-170.3645f, -533.8325f, 57.00174f));
            StartToEnd.Add(new Vector3(-168.6642f, -532.9321f, 56.99762f));
            StartToEnd.Add(new Vector3(-167.0488f, -532.0963f, 56.99445f));
            StartToEnd.Add(new Vector3(-165.3289f, -531.3279f, 56.9897f));
            StartToEnd.Add(new Vector3(-163.418f, -530.4741f, 56.98422f));
            StartToEnd.Add(new Vector3(-161.7643f, -529.4835f, 57.56956f));
            StartToEnd.Add(new Vector3(-160.0747f, -528.3463f, 57.95463f));
            StartToEnd.Add(new Vector3(-158.6944f, -527.4129f, 57.95536f));
            StartToEnd.Add(new Vector3(-157.2824f, -525.9902f, 57.95536f));
            StartToEnd.Add(new Vector3(-156.477f, -524.3691f, 57.95536f));
            StartToEnd.Add(new Vector3(-156.3577f, -522.4109f, 57.9555f));
            StartToEnd.Add(new Vector3(-157.2825f, -520.894f, 57.9555f));
            StartToEnd.Add(new Vector3(-158.8202f, -519.7144f, 57.95063f));
            StartToEnd.Add(new Vector3(-160.6239f, -518.8664f, 57.94744f));
            StartToEnd.Add(new Vector3(-162.4285f, -518.299f, 57.95501f));
            StartToEnd.Add(new Vector3(-164.1862f, -517.8341f, 57.95668f));
            StartToEnd.Add(new Vector3(-166.248f, -517.3903f, 57.95668f));
            StartToEnd.Add(new Vector3(-168.0659f, -516.9988f, 57.95668f));
            StartToEnd.Add(new Vector3(-169.9468f, -516.5939f, 57.95668f));
            StartToEnd.Add(new Vector3(-171.804f, -516.1941f, 57.95348f));
            StartToEnd.Add(new Vector3(-173.5251f, -515.8126f, 57.95536f));
            StartToEnd.Add(new Vector3(-175.3383f, -515.2783f, 57.95536f));
            StartToEnd.Add(new Vector3(-176.9465f, -514.4179f, 57.95536f));
            StartToEnd.Add(new Vector3(-178.4291f, -513.2472f, 57.95536f));
            StartToEnd.Add(new Vector3(-179.5837f, -511.9107f, 57.95536f));
            StartToEnd.Add(new Vector3(-180.6348f, -510.1491f, 57.95232f));
            StartToEnd.Add(new Vector3(-181.4066f, -508.6633f, 57.9458f));
            StartToEnd.Add(new Vector3(-182.3307f, -506.8845f, 57.94221f));
            StartToEnd.Add(new Vector3(-183.1471f, -505.3129f, 57.94221f));
            StartToEnd.Add(new Vector3(-183.9746f, -503.7198f, 57.85538f));
            StartToEnd.Add(new Vector3(-184.8727f, -501.9911f, 57.48373f));
            StartToEnd.Add(new Vector3(-185.8594f, -500.2122f, 57.48232f));
            StartToEnd.Add(new Vector3(-186.8823f, -498.8461f, 57.46991f));
            StartToEnd.Add(new Vector3(-188.4161f, -497.5036f, 57.43331f));
            StartToEnd.Add(new Vector3(-190.0687f, -496.5869f, 57.35813f));
            StartToEnd.Add(new Vector3(-193.6768f, -495.0263f, 57.22665f));
            StartToEnd.Add(new Vector3(-197.5686f, -494.2964f, 57.28115f));
            StartToEnd.Add(new Vector3(-200.6449f, -494.923f, 57.35502f));
            StartToEnd.Add(new Vector3(-202.9826f, -495.7487f, 57.7088f));
            StartToEnd.Add(new Vector3(-206.1822f, -497.6123f, 57.43249f));
            StartToEnd.Add(new Vector3(-208.6703f, -500.2379f, 57.23275f));
            StartToEnd.Add(new Vector3(-210.7376f, -503.117f, 57.01381f));
            StartToEnd.Add(new Vector3(-212.9433f, -506.4721f, 56.87976f));
            StartToEnd.Add(new Vector3(-215.1938f, -509.8983f, 56.78913f));
            StartToEnd.Add(new Vector3(-217.2045f, -512.9594f, 56.57505f));
            StartToEnd.Add(new Vector3(-219.3536f, -516.2311f, 56.69599f));
            StartToEnd.Add(new Vector3(-221.6226f, -519.6853f, 57.01536f));
            StartToEnd.Add(new Vector3(-224.0185f, -523.3326f, 57.19843f));
            StartToEnd.Add(new Vector3(-226.2971f, -526.4513f, 57.17331f));
            StartToEnd.Add(new Vector3(-228.8064f, -529.2321f, 56.91927f));
            StartToEnd.Add(new Vector3(-231.5691f, -531.8085f, 56.66055f));
            StartToEnd.Add(new Vector3(-234.7724f, -534.254f, 56.76919f));
            StartToEnd.Add(new Vector3(-238.0073f, -536.2027f, 57.02345f));
            StartToEnd.Add(new Vector3(-241.5511f, -537.8637f, 57.19135f));
            StartToEnd.Add(new Vector3(-245.1155f, -539.3554f, 56.88221f));
            StartToEnd.Add(new Vector3(-248.9492f, -540.9434f, 56.05128f));
            StartToEnd.Add(new Vector3(-252.3483f, -542.3514f, 55.20089f));
            StartToEnd.Add(new Vector3(-256.1665f, -543.933f, 54.35065f));
            StartToEnd.Add(new Vector3(-259.9097f, -545.5178f, 53.57199f));
            StartToEnd.Add(new Vector3(-263.4265f, -547.4548f, 52.8962f));
            StartToEnd.Add(new Vector3(-267.131f, -549.5712f, 52.57318f));
            StartToEnd.Add(new Vector3(-270.2827f, -551.748f, 52.386f));
            StartToEnd.Add(new Vector3(-273.1988f, -553.7635f, 52.29091f));
            StartToEnd.Add(new Vector3(-276.875f, -556.3044f, 52.12853f));
            StartToEnd.Add(new Vector3(-280.0675f, -558.5109f, 52.06841f));
            StartToEnd.Add(new Vector3(-283.2876f, -560.7365f, 51.999f));
            StartToEnd.Add(new Vector3(-286.2411f, -562.8419f, 51.8199f));
            StartToEnd.Add(new Vector3(-289.1436f, -565.5917f, 51.06287f));
            StartToEnd.Add(new Vector3(-292.3406f, -568.6416f, 49.7458f));
            StartToEnd.Add(new Vector3(-295.1f, -571.2739f, 49.22601f));
            StartToEnd.Add(new Vector3(-297.7135f, -573.7672f, 48.55618f));
            StartToEnd.Add(new Vector3(-300.5526f, -576.8563f, 47.73601f));
            StartToEnd.Add(new Vector3(-303.1023f, -580.0009f, 47.25204f));
            StartToEnd.Add(new Vector3(-305.0987f, -582.5131f, 47.3888f));
            StartToEnd.Add(new Vector3(-307.1362f, -584.7693f, 48.53452f));
            StartToEnd.Add(new Vector3(-310.3031f, -587.7955f, 49.07732f));
            StartToEnd.Add(new Vector3(-313.1665f, -590.2109f, 49.42923f));
            StartToEnd.Add(new Vector3(-316.2686f, -592.5144f, 49.71102f));
            StartToEnd.Add(new Vector3(-319.5259f, -594.8026f, 49.94153f));
            StartToEnd.Add(new Vector3(-322.2591f, -596.2772f, 50.14092f));
            StartToEnd.Add(new Vector3(-325.6914f, -598.0853f, 50.43975f));
            StartToEnd.Add(new Vector3(-328.6558f, -599.8384f, 50.66648f));
            StartToEnd.Add(new Vector3(-331.7647f, -601.6771f, 51.16039f));
            StartToEnd.Add(new Vector3(-335.2208f, -603.7211f, 52.19299f));
            StartToEnd.Add(new Vector3(-338.1718f, -605.6503f, 53.06017f));
            StartToEnd.Add(new Vector3(-341.8859f, -608.4261f, 54.20434f));
            StartToEnd.Add(new Vector3(-345.1102f, -610.847f, 55.3688f));
            StartToEnd.Add(new Vector3(-348.1599f, -613.1368f, 55.60859f));
            StartToEnd.Add(new Vector3(-351.5588f, -615.6889f, 55.63159f));
            StartToEnd.Add(new Vector3(-354.8906f, -618.1906f, 55.85055f));
            StartToEnd.Add(new Vector3(-357.7949f, -620.3936f, 56.10569f));
            StartToEnd.Add(new Vector3(-363.841f, -627.8594f, 57.21904f));
            StartToEnd.Add(new Vector3(-364.4573f, -627.2404f, 57.08265f));
            StartToEnd.Add(new Vector3(-365.9166f, -624.0738f, 56.7133f));
            StartToEnd.Add(new Vector3(-364.6141f, -620.9174f, 56.74101f));
            StartToEnd.Add(new Vector3(-364.5318f, -618.0119f, 57.03508f));
            StartToEnd.Add(new Vector3(-368.683f, -619.091f, 57.37798f));
            StartToEnd.Add(new Vector3(-372.0075f, -620.667f, 57.29012f));
            StartToEnd.Add(new Vector3(-375.5296f, -622.3359f, 57.57764f));
            StartToEnd.Add(new Vector3(-379.1172f, -624.1311f, 57.95111f));
            StartToEnd.Add(new Vector3(-382.5462f, -626.3836f, 58.54605f));
            StartToEnd.Add(new Vector3(-385.7355f, -628.5603f, 59.2043f));
            StartToEnd.Add(new Vector3(-388.3441f, -631.5064f, 59.72848f));
            StartToEnd.Add(new Vector3(-389.4861f, -635.9537f, 60.0622f));
            StartToEnd.Add(new Vector3(-389.3247f, -639.9967f, 60.07853f));
            StartToEnd.Add(new Vector3(-389.066f, -643.65f, 60.38255f));
            StartToEnd.Add(new Vector3(-388.7044f, -647.8661f, 60.99747f));
            StartToEnd.Add(new Vector3(-387.8803f, -651.6396f, 61.62518f));
            StartToEnd.Add(new Vector3(-386.6117f, -655.1641f, 62.36417f));
            StartToEnd.Add(new Vector3(-385.2517f, -658.7629f, 63.10085f));
            StartToEnd.Add(new Vector3(-383.5679f, -662.647f, 63.80761f));
            StartToEnd.Add(new Vector3(-381.9783f, -666.2424f, 64.40968f));
            StartToEnd.Add(new Vector3(-380.4221f, -669.4419f, 64.98841f));
            StartToEnd.Add(new Vector3(-378.3987f, -673.0455f, 65.46559f));
            StartToEnd.Add(new Vector3(-376.0898f, -676.202f, 65.66629f));
            StartToEnd.Add(new Vector3(-373.5959f, -679.2408f, 65.82062f));
            StartToEnd.Add(new Vector3(-371.1659f, -682.2017f, 65.78234f));
            StartToEnd.Add(new Vector3(-368.6719f, -685.2405f, 65.80558f));
            StartToEnd.Add(new Vector3(-367.1531f, -687.0345f, 65.99256f));
            StartToEnd.Add(new Vector3(-362.903f, -689.9266f, 65.42783f));
            StartToEnd.Add(new Vector3(-360.4072f, -692.5329f, 64.10557f));
            StartToEnd.Add(new Vector3(-358.7889f, -696.3793f, 65.61174f));
            StartToEnd.Add(new Vector3(-357.586f, -699.6771f, 66.15687f));
            StartToEnd.Add(new Vector3(-355.7536f, -703.1351f, 66.39022f));
            StartToEnd.Add(new Vector3(-353.8601f, -706.2866f, 66.77454f));
            StartToEnd.Add(new Vector3(-351.5861f, -709.1341f, 67.08801f));
            StartToEnd.Add(new Vector3(-349.0447f, -712.1334f, 67.0446f));
            StartToEnd.Add(new Vector3(-346.8197f, -715.0756f, 67.01977f));
            StartToEnd.Add(new Vector3(-345.3077f, -718.28f, 67.09626f));
            StartToEnd.Add(new Vector3(-344.0008f, -722.0087f, 67.08088f));
            StartToEnd.Add(new Vector3(-343.3652f, -724.4888f, 67.07426f));
            StartToEnd.Add(new Vector3(-342.2079f, -729.394f, 66.78685f));
            StartToEnd.Add(new Vector3(-341.3013f, -733.2366f, 66.67826f));
            StartToEnd.Add(new Vector3(-340.3561f, -737.2426f, 66.66526f));
            StartToEnd.Add(new Vector3(-339.4919f, -740.9052f, 66.93419f));
            StartToEnd.Add(new Vector3(-338.402f, -744.8897f, 67.59991f));
            StartToEnd.Add(new Vector3(-336.9034f, -748.2543f, 67.82072f));
            StartToEnd.Add(new Vector3(-334.9491f, -751.7811f, 67.61674f));
            StartToEnd.Add(new Vector3(-333.0329f, -755.116f, 67.22654f));
            StartToEnd.Add(new Vector3(-331.1184f, -758.1591f, 67.26785f));
            StartToEnd.Add(new Vector3(-328.9892f, -761.5434f, 67.2076f));
            StartToEnd.Add(new Vector3(-326.7347f, -765.1268f, 67.1703f));
            StartToEnd.Add(new Vector3(-324.7486f, -768.2836f, 66.44148f));
            StartToEnd.Add(new Vector3(-322.7447f, -771.4689f, 65.18935f));
            StartToEnd.Add(new Vector3(-320.5071f, -775.043f, 63.86913f));
            StartToEnd.Add(new Vector3(-318.7747f, -778.1907f, 63.50151f));
            StartToEnd.Add(new Vector3(-317.1916f, -782.0066f, 62.97767f));
            StartToEnd.Add(new Vector3(-316.165f, -785.6057f, 62.50793f));
            StartToEnd.Add(new Vector3(-315.559f, -789.4202f, 62.13495f));
            StartToEnd.Add(new Vector3(-315.389f, -792.976f, 61.92143f));
            StartToEnd.Add(new Vector3(-315.3415f, -797.0077f, 61.50887f));
            StartToEnd.Add(new Vector3(-315.2975f, -800.7371f, 61.00173f));
            StartToEnd.Add(new Vector3(-315.2519f, -804.6008f, 60.24333f));
            StartToEnd.Add(new Vector3(-315.9099f, -808.3815f, 59.16201f));
            StartToEnd.Add(new Vector3(-317.0096f, -812.1557f, 58.08523f));
            StartToEnd.Add(new Vector3(-319.2527f, -815.0009f, 57.4408f));
            StartToEnd.Add(new Vector3(-322.6446f, -817.0167f, 56.61464f));
            StartToEnd.Add(new Vector3(-327.6207f, -818.9977f, 54.96348f));
            StartToEnd.Add(new Vector3(-331.3351f, -820.3847f, 53.56698f));
            StartToEnd.Add(new Vector3(-335.9937f, -822.1242f, 51.86602f));
            StartToEnd.Add(new Vector3(-339.5349f, -823.4465f, 51.13488f));
            StartToEnd.Add(new Vector3(-342.7165f, -824.7941f, 50.61845f));
            StartToEnd.Add(new Vector3(-346.0237f, -827.2309f, 50.22205f));
            StartToEnd.Add(new Vector3(-348.7328f, -830.2553f, 50.07851f));
            StartToEnd.Add(new Vector3(-350.9673f, -833.2413f, 50.13812f));
            StartToEnd.Add(new Vector3(-353.1313f, -837.1157f, 50.01009f));
            StartToEnd.Add(new Vector3(-354.4003f, -840.6736f, 49.79338f));
            StartToEnd.Add(new Vector3(-355.6253f, -844.6559f, 49.84878f));
            StartToEnd.Add(new Vector3(-356.6824f, -848.0922f, 50.2352f));
            StartToEnd.Add(new Vector3(-357.8679f, -851.9459f, 50.61409f));
            StartToEnd.Add(new Vector3(-358.7916f, -854.9487f, 50.72728f));
            StartToEnd.Add(new Vector3(-359.7412f, -858.5688f, 50.47816f));
            StartToEnd.Add(new Vector3(-360.4967f, -862.5294f, 50.58554f));
            StartToEnd.Add(new Vector3(-361.1672f, -866.0444f, 50.87276f));
            StartToEnd.Add(new Vector3(-361.8566f, -869.6584f, 50.81429f));
            StartToEnd.Add(new Vector3(-362.6026f, -873.5696f, 50.5276f));
            StartToEnd.Add(new Vector3(-363.3266f, -877.3651f, 50.19077f));
            StartToEnd.Add(new Vector3(-364.2741f, -881.5592f, 49.83507f));
            StartToEnd.Add(new Vector3(-365.3073f, -885.2973f, 49.55474f));
            StartToEnd.Add(new Vector3(-366.7323f, -889.3552f, 49.31325f));
            StartToEnd.Add(new Vector3(-367.9234f, -892.7473f, 49.18154f));
            StartToEnd.Add(new Vector3(-369.1035f, -896.1077f, 49.13894f));
            StartToEnd.Add(new Vector3(-370.3058f, -899.5316f, 49.15486f));
            StartToEnd.Add(new Vector3(-371.6194f, -903.2724f, 49.23342f));
            StartToEnd.Add(new Vector3(-372.9635f, -906.9665f, 49.36196f));
            StartToEnd.Add(new Vector3(-374.4498f, -910.748f, 49.55185f));
            StartToEnd.Add(new Vector3(-375.8348f, -914.1384f, 49.69798f));
            StartToEnd.Add(new Vector3(-377.365f, -917.9594f, 49.75914f));
            StartToEnd.Add(new Vector3(-378.7578f, -921.4373f, 49.67348f));
            StartToEnd.Add(new Vector3(-380.213f, -925.0711f, 49.64242f));
            StartToEnd.Add(new Vector3(-381.3191f, -928.6949f, 49.58262f));
            StartToEnd.Add(new Vector3(-382.1152f, -932.011f, 49.35384f));
            StartToEnd.Add(new Vector3(-382.9976f, -935.6866f, 49.25023f));
            StartToEnd.Add(new Vector3(-383.9153f, -939.5092f, 49.10789f));
            StartToEnd.Add(new Vector3(-384.908f, -943.3298f, 48.90763f));
            StartToEnd.Add(new Vector3(-386.0269f, -947.2385f, 48.71258f));
            StartToEnd.Add(new Vector3(-387.081f, -950.921f, 48.73848f));
            StartToEnd.Add(new Vector3(-388.2693f, -955.0718f, 49.00532f));
            StartToEnd.Add(new Vector3(-389.2494f, -958.4959f, 49.19357f));
            StartToEnd.Add(new Vector3(-390.2527f, -962.0007f, 49.2202f));
            StartToEnd.Add(new Vector3(-391.4502f, -966.1839f, 49.33996f));
            StartToEnd.Add(new Vector3(-392.3333f, -969.2688f, 49.47693f));
            StartToEnd.Add(new Vector3(-393.4383f, -973.1289f, 49.47052f));
            StartToEnd.Add(new Vector3(-394.5895f, -977.1506f, 49.4649f));
            StartToEnd.Add(new Vector3(-395.6899f, -980.9946f, 49.50982f));
            StartToEnd.Add(new Vector3(-396.7256f, -984.6125f, 49.70113f));
            StartToEnd.Add(new Vector3(-397.5431f, -988.194f, 49.91031f));
            StartToEnd.Add(new Vector3(-397.9297f, -992.1697f, 50.05544f));
            StartToEnd.Add(new Vector3(-397.5344f, -995.9393f, 50.15411f));
            StartToEnd.Add(new Vector3(-396.741f, -999.8752f, 50.28284f));
            StartToEnd.Add(new Vector3(-395.9267f, -1003.807f, 50.43496f));
            StartToEnd.Add(new Vector3(-395.1056f, -1007.772f, 50.5756f));
            StartToEnd.Add(new Vector3(-394.2538f, -1011.884f, 50.71638f));
            StartToEnd.Add(new Vector3(-393.4498f, -1015.767f, 50.76321f));
            StartToEnd.Add(new Vector3(-392.6251f, -1019.582f, 50.77848f));
            StartToEnd.Add(new Vector3(-391.6449f, -1023.528f, 50.80267f));
            StartToEnd.Add(new Vector3(-390.5277f, -1027.366f, 50.76465f));
            StartToEnd.Add(new Vector3(-389.3276f, -1030.843f, 50.67506f));
            StartToEnd.Add(new Vector3(-387.9632f, -1034.744f, 50.65516f));
            StartToEnd.Add(new Vector3(-386.7279f, -1038.388f, 50.74345f));
            StartToEnd.Add(new Vector3(-385.4206f, -1042.255f, 50.84031f));
            StartToEnd.Add(new Vector3(-384.0919f, -1046.187f, 50.98205f));
            StartToEnd.Add(new Vector3(-382.9622f, -1049.529f, 51.17109f));
            StartToEnd.Add(new Vector3(-381.6236f, -1053.489f, 51.45088f));
            StartToEnd.Add(new Vector3(-380.4237f, -1057.056f, 51.82241f));
            StartToEnd.Add(new Vector3(-379.2285f, -1060.73f, 52.38095f));
            StartToEnd.Add(new Vector3(-378.0952f, -1064.407f, 52.58431f));
            StartToEnd.Add(new Vector3(-376.9199f, -1068.281f, 52.35508f));
            StartToEnd.Add(new Vector3(-375.764f, -1072.091f, 51.77074f));
            StartToEnd.Add(new Vector3(-374.6228f, -1075.853f, 51.38892f));
            StartToEnd.Add(new Vector3(-373.4572f, -1079.695f, 51.38892f));
            StartToEnd.Add(new Vector3(-372.3891f, -1083.216f, 51.38892f));
            StartToEnd.Add(new Vector3(-371.326f, -1086.721f, 51.38892f));
            StartToEnd.Add(new Vector3(-370.1642f, -1090.388f, 51.38892f));
            StartToEnd.Add(new Vector3(-369.0237f, -1093.762f, 51.38892f));
            StartToEnd.Add(new Vector3(-367.6259f, -1097.489f, 51.41619f));
            StartToEnd.Add(new Vector3(-366.0771f, -1100.992f, 51.50117f));
            StartToEnd.Add(new Vector3(-364.4595f, -1104.427f, 51.50117f));
            StartToEnd.Add(new Vector3(-362.9231f, -1107.659f, 51.50117f));
            StartToEnd.Add(new Vector3(-361.2137f, -1111.255f, 51.50117f));
            StartToEnd.Add(new Vector3(-359.4609f, -1114.942f, 51.50117f));
            StartToEnd.Add(new Vector3(-357.8092f, -1118.416f, 51.50117f));
            StartToEnd.Add(new Vector3(-356.2179f, -1121.771f, 51.50117f));
            StartToEnd.Add(new Vector3(-354.6277f, -1125.529f, 51.50117f));
            StartToEnd.Add(new Vector3(-353.3798f, -1129.256f, 51.50117f));
            StartToEnd.Add(new Vector3(-352.4216f, -1133.102f, 51.42909f));
            StartToEnd.Add(new Vector3(-351.8232f, -1137.12f, 51.27856f));
            StartToEnd.Add(new Vector3(-351.6974f, -1141.166f, 51.18061f));
            StartToEnd.Add(new Vector3(-351.7512f, -1144.712f, 51.15171f));
            StartToEnd.Add(new Vector3(-351.954f, -1148.738f, 51.13051f));
            StartToEnd.Add(new Vector3(-352.2219f, -1152.997f, 51.18317f));
            StartToEnd.Add(new Vector3(-352.4571f, -1156.736f, 51.41682f));
            StartToEnd.Add(new Vector3(-352.7102f, -1160.76f, 51.69249f));
            StartToEnd.Add(new Vector3(-352.7804f, -1164.524f, 51.83775f));
            StartToEnd.Add(new Vector3(-352.0868f, -1168.608f, 52.07372f));
            StartToEnd.Add(new Vector3(-350.7149f, -1172.643f, 52.47031f));
            StartToEnd.Add(new Vector3(-349.22f, -1176.059f, 52.47768f));
            StartToEnd.Add(new Vector3(-347.4194f, -1179.835f, 51.94154f));
            StartToEnd.Add(new Vector3(-345.9213f, -1182.955f, 51.73339f));
            StartToEnd.Add(new Vector3(-344.2486f, -1186.438f, 51.96218f));
            StartToEnd.Add(new Vector3(-342.7699f, -1189.856f, 52.31795f));
            StartToEnd.Add(new Vector3(-341.709f, -1193.554f, 52.75011f));
            StartToEnd.Add(new Vector3(-340.7155f, -1197.025f, 52.89695f));
            StartToEnd.Add(new Vector3(-339.769f, -1200.632f, 53.06138f));
            StartToEnd.Add(new Vector3(-338.9757f, -1203.88f, 53.40474f));
            StartToEnd.Add(new Vector3(-338.186f, -1207.473f, 54.055f));
            StartToEnd.Add(new Vector3(-337.5299f, -1210.442f, 55.36862f));
            StartToEnd.Add(new Vector3(-336.1625f, -1214.098f, 58.03228f));
            StartToEnd.Add(new Vector3(-333.8844f, -1216.955f, 60.58561f));
            StartToEnd.Add(new Vector3(-330.9664f, -1218.559f, 60.73244f));
            StartToEnd.Add(new Vector3(-327.1145f, -1219.223f, 61.09005f));
            StartToEnd.Add(new Vector3(-323.2139f, -1219.177f, 62.65434f));
            StartToEnd.Add(new Vector3(-319.6771f, -1218.384f, 64.2899f));
            StartToEnd.Add(new Vector3(-316.1858f, -1217.223f, 65.29748f));
            StartToEnd.Add(new Vector3(-312.6018f, -1215.831f, 66.28771f));
            StartToEnd.Add(new Vector3(-309.1694f, -1214.329f, 67.7328f));
            StartToEnd.Add(new Vector3(-305.306f, -1212.639f, 69.29769f));
            StartToEnd.Add(new Vector3(-301.8157f, -1211.103f, 70.41392f));
            StartToEnd.Add(new Vector3(-298.2817f, -1209.54f, 71.03732f));
            StartToEnd.Add(new Vector3(-294.6941f, -1207.893f, 71.38352f));
            StartToEnd.Add(new Vector3(-291.3894f, -1206.353f, 71.65869f));
            StartToEnd.Add(new Vector3(-287.7802f, -1204.672f, 71.93244f));
            StartToEnd.Add(new Vector3(-284.5821f, -1203.182f, 72.05857f));
            StartToEnd.Add(new Vector3(-280.7391f, -1201.532f, 72.03872f));
            StartToEnd.Add(new Vector3(-276.9623f, -1200.174f, 72.19885f));
            StartToEnd.Add(new Vector3(-272.9926f, -1199.21f, 72.35378f));
            StartToEnd.Add(new Vector3(-269.0644f, -1199.014f, 72.41442f));
            StartToEnd.Add(new Vector3(-265.1697f, -1200.061f, 72.37211f));
            StartToEnd.Add(new Vector3(-261.526f, -1201.662f, 72.34871f));
            StartToEnd.Add(new Vector3(-258.3209f, -1203.773f, 72.35945f));
            StartToEnd.Add(new Vector3(-255.5471f, -1206.426f, 72.47785f));
            StartToEnd.Add(new Vector3(-253.5919f, -1209.756f, 72.64499f));
            StartToEnd.Add(new Vector3(-252.4048f, -1213.307f, 72.81355f));
            StartToEnd.Add(new Vector3(-251.7106f, -1217.356f, 73.0332f));
            StartToEnd.Add(new Vector3(-251.3355f, -1221.472f, 73.25905f));
            StartToEnd.Add(new Vector3(-251.0852f, -1225.327f, 73.37054f));
            StartToEnd.Add(new Vector3(-251.2186f, -1229.504f, 73.37054f));
            StartToEnd.Add(new Vector3(-252.1046f, -1233.219f, 73.32658f));
            StartToEnd.Add(new Vector3(-253.9897f, -1236.908f, 73.32658f));
            StartToEnd.Add(new Vector3(-255.9291f, -1240.309f, 73.32658f));
            StartToEnd.Add(new Vector3(-257.7186f, -1243.446f, 73.32658f));
            StartToEnd.Add(new Vector3(-259.6982f, -1246.802f, 73.33569f));
            StartToEnd.Add(new Vector3(-262.0353f, -1249.588f, 73.30585f));
            StartToEnd.Add(new Vector3(-265.0146f, -1251.737f, 73.3479f));
            StartToEnd.Add(new Vector3(-267.9888f, -1253.7f, 72.91869f));
            StartToEnd.Add(new Vector3(-271.1964f, -1255.944f, 72.93019f));
            StartToEnd.Add(new Vector3(-274.5038f, -1258.446f, 73.19897f));
            StartToEnd.Add(new Vector3(-277.4109f, -1260.966f, 73.98801f));
            StartToEnd.Add(new Vector3(-279.9677f, -1263.888f, 75.30948f));
            StartToEnd.Add(new Vector3(-282.2814f, -1267.112f, 77.4812f));
            StartToEnd.Add(new Vector3(-284.1664f, -1270.427f, 80.32241f));
            StartToEnd.Add(new Vector3(-286.0197f, -1273.799f, 83.2555f));
            StartToEnd.Add(new Vector3(-287.8513f, -1277.22f, 85.74345f));
            StartToEnd.Add(new Vector3(-289.3184f, -1280.682f, 87.53102f));
            StartToEnd.Add(new Vector3(-290.1297f, -1283.975f, 88.62383f));
            StartToEnd.Add(new Vector3(-290.8054f, -1288.035f, 89.70778f));
            StartToEnd.Add(new Vector3(-291.1563f, -1291.859f, 90.40313f));
            StartToEnd.Add(new Vector3(-291.2781f, -1295.738f, 90.45673f));
            StartToEnd.Add(new Vector3(-291.3958f, -1299.483f, 90.54458f));
            StartToEnd.Add(new Vector3(-291.5203f, -1303.446f, 90.66599f));
            StartToEnd.Add(new Vector3(-291.5884f, -1307.275f, 90.67439f));
            StartToEnd.Add(new Vector3(-291.5392f, -1311.441f, 90.56055f));
            StartToEnd.Add(new Vector3(-291.4935f, -1315.322f, 90.42002f));
            StartToEnd.Add(new Vector3(-291.4459f, -1319.353f, 90.48052f));
            StartToEnd.Add(new Vector3(-291.4263f, -1323.005f, 90.6272f));
            StartToEnd.Add(new Vector3(-291.4473f, -1327.255f, 90.68183f));
            StartToEnd.Add(new Vector3(-291.5726f, -1331.117f, 90.72683f));
            StartToEnd.Add(new Vector3(-291.9477f, -1335.104f, 90.86484f));
            StartToEnd.Add(new Vector3(-292.7419f, -1338.849f, 91.02266f));
            StartToEnd.Add(new Vector3(-293.9333f, -1342.7f, 91.11864f));
            StartToEnd.Add(new Vector3(-295.1671f, -1346.397f, 90.98096f));
            StartToEnd.Add(new Vector3(-296.5531f, -1349.918f, 90.85419f));
            StartToEnd.Add(new Vector3(-298.2696f, -1353.677f, 90.85371f));
            StartToEnd.Add(new Vector3(-300.1454f, -1357.016f, 91.22847f));
            StartToEnd.Add(new Vector3(-302.0652f, -1360.291f, 91.50842f));
            StartToEnd.Add(new Vector3(-303.9936f, -1363.581f, 91.59072f));
            StartToEnd.Add(new Vector3(-305.769f, -1366.611f, 91.50319f));
            StartToEnd.Add(new Vector3(-307.7058f, -1369.915f, 91.15814f));
            StartToEnd.Add(new Vector3(-309.099f, -1372.292f, 91.27774f), WaypointAction.Mount);
            waypoints = StartToEnd;

            EndCircle.Add(new Vector3(-298.756f, -1353.752f, 90.91322f));
            EndCircle.Add(new Vector3(-297.7f, -1352.068f, 90.7314f));
            EndCircle.Add(new Vector3(-296.7437f, -1350.445f, 90.80034f));
            EndCircle.Add(new Vector3(-296.063f, -1348.997f, 90.84323f));
            EndCircle.Add(new Vector3(-295.4182f, -1347.478f, 90.89973f));
            EndCircle.Add(new Vector3(-295.3734f, -1345.922f, 91.0186f));
            EndCircle.Add(new Vector3(-295.5327f, -1344.086f, 91.17212f));
            EndCircle.Add(new Vector3(-296.0082f, -1342.605f, 91.3717f));
            EndCircle.Add(new Vector3(-297.4396f, -1341.26f, 91.59448f));
            EndCircle.Add(new Vector3(-298.9545f, -1340.282f, 91.72314f));
            EndCircle.Add(new Vector3(-300.5726f, -1339.737f, 91.78163f));
            EndCircle.Add(new Vector3(-302.3812f, -1339.424f, 91.80363f));
            EndCircle.Add(new Vector3(-304.0215f, -1339.183f, 91.89686f));
            EndCircle.Add(new Vector3(-305.4191f, -1339.182f, 91.84189f));
            EndCircle.Add(new Vector3(-307.3367f, -1339.722f, 91.88229f));
            EndCircle.Add(new Vector3(-308.7606f, -1340.73f, 92.01289f));
            EndCircle.Add(new Vector3(-310.0632f, -1341.96f, 91.99515f));
            EndCircle.Add(new Vector3(-310.9467f, -1343.727f, 91.93484f));
            EndCircle.Add(new Vector3(-311.494f, -1344.892f, 91.92381f));
            EndCircle.Add(new Vector3(-312.2927f, -1347.529f, 91.92107f));
            EndCircle.Add(new Vector3(-312.6563f, -1349.188f, 91.84916f));
            EndCircle.Add(new Vector3(-312.6685f, -1351.194f, 91.9728f));
            EndCircle.Add(new Vector3(-312.3286f, -1353.02f, 92.15938f));
            EndCircle.Add(new Vector3(-311.3496f, -1354.819f, 92.18842f));
            EndCircle.Add(new Vector3(-310.109f, -1356.007f, 92.08553f));
            EndCircle.Add(new Vector3(-307.7586f, -1357.126f, 91.94891f));
            EndCircle.Add(new Vector3(-305.938f, -1357.535f, 91.77636f));
            EndCircle.Add(new Vector3(-304.4128f, -1357.534f, 91.64425f));
            EndCircle.Add(new Vector3(-303.0632f, -1356.221f, 91.61167f));
            EndCircle.Add(new Vector3(-301.8603f, -1354.835f, 91.41417f));
            EndCircle.Add(new Vector3(-300.7577f, -1353.564f, 91.2722f));
            EndCircle.Add(new Vector3(-299.3807f, -1351.977f, 91.14781f));
            EndCircle.Add(new Vector3(-298.067f, -1350.463f, 91.06172f));
            EndCircle.Add(new Vector3(-297.0425f, -1349.053f, 91.07665f));
            EndCircle.Add(new Vector3(-296.1747f, -1347.546f, 90.99585f));
            EndCircle.Add(new Vector3(-295.5972f, -1346.07f, 91.0251f));
            EndCircle.Add(new Vector3(-295.8723f, -1344.118f, 91.21076f));
            EndCircle.Add(new Vector3(-296.4045f, -1342.897f, 91.43919f));
            EndCircle.Add(new Vector3(-297.9671f, -1341.253f, 91.66042f));
            EndCircle.Add(new Vector3(-299.7908f, -1340.331f, 91.8005f));
            EndCircle.Add(new Vector3(-301.548f, -1339.4f, 91.77754f));
            EndCircle.Add(new Vector3(-303.1952f, -1338.696f, 91.73953f));
            EndCircle.Add(new Vector3(-304.9142f, -1338.246f, 91.70015f));
            EndCircle.Add(new Vector3(-306.6352f, -1338.272f, 91.67088f));
            EndCircle.Add(new Vector3(-308.198f, -1338.876f, 91.74338f));
            EndCircle.Add(new Vector3(-309.7023f, -1340.047f, 91.86362f));
            EndCircle.Add(new Vector3(-310.6035f, -1341.682f, 91.93086f));
            EndCircle.Add(new Vector3(-311.3223f, -1343.412f, 91.88684f));
            EndCircle.Add(new Vector3(-312.242f, -1345.166f, 91.82039f));
            EndCircle.Add(new Vector3(-312.9339f, -1346.555f, 91.76189f));
            EndCircle.Add(new Vector3(-313.2181f, -1348.635f, 91.76367f));
            EndCircle.Add(new Vector3(-313.071f, -1350.486f, 91.84974f));
            EndCircle.Add(new Vector3(-312.1937f, -1352.258f, 92.07938f));
            EndCircle.Add(new Vector3(-310.9764f, -1353.776f, 92.13974f));
            EndCircle.Add(new Vector3(-309.4462f, -1354.937f, 92.05743f));
            EndCircle.Add(new Vector3(-307.5296f, -1355.972f, 91.9233f));
            EndCircle.Add(new Vector3(-305.6683f, -1356.57f, 91.75519f));
            EndCircle.Add(new Vector3(-304.0773f, -1356.651f, 91.6435f));
            EndCircle.Add(new Vector3(-302.4206f, -1356.182f, 91.58604f));
            EndCircle.Add(new Vector3(-300.9006f, -1355.397f, 91.30389f));
            EndCircle.Add(new Vector3(-299.3654f, -1354.098f, 90.99915f));
            EndCircle.Add(new Vector3(-298.3977f, -1353.13f, 90.84345f));
            EndCircle.Add(new Vector3(-296.8405f, -1351.569f, 90.70487f));
            EndCircle.Add(new Vector3(-295.993f, -1350.09f, 90.71265f));
            EndCircle.Add(new Vector3(-295.323f, -1348.382f, 90.81997f));
            EndCircle.Add(new Vector3(-294.9261f, -1346.641f, 90.95218f));
            EndCircle.Add(new Vector3(-294.9709f, -1345.347f, 91.01917f));
            EndCircle.Add(new Vector3(-295.5526f, -1343.221f, 91.24879f));
            EndCircle.Add(new Vector3(-296.9738f, -1341.846f, 91.55972f));
            EndCircle.Add(new Vector3(-298.4741f, -1340.616f, 91.69236f));
            EndCircle.Add(new Vector3(-299.7975f, -1339.568f, 91.73397f));
            EndCircle.Add(new Vector3(-301.058f, -1339.155f, 91.74307f));
            EndCircle.Add(new Vector3(-303.2706f, -1339.107f, 91.81998f));
            EndCircle.Add(new Vector3(-304.8792f, -1339.542f, 91.93559f));
            EndCircle.Add(new Vector3(-306.5118f, -1340.079f, 91.95181f));
            EndCircle.Add(new Vector3(-308.0599f, -1340.921f, 92.06355f));
            EndCircle.Add(new Vector3(-309.295f, -1341.808f, 92.07636f));
            EndCircle.Add(new Vector3(-310.5291f, -1343.168f, 91.9695f));
            EndCircle.Add(new Vector3(-311.4028f, -1344.819f, 91.93962f));
            EndCircle.Add(new Vector3(-312.0218f, -1346.495f, 92.01065f));
            EndCircle.Add(new Vector3(-312.6572f, -1348.174f, 91.82896f));
            EndCircle.Add(new Vector3(-312.7066f, -1349.714f, 91.85158f));
            EndCircle.Add(new Vector3(-311.9008f, -1351.706f, 92.01f));
            EndCircle.Add(new Vector3(-310.6115f, -1352.943f, 92.04301f));
            EndCircle.Add(new Vector3(-309.2278f, -1354.156f, 92.05402f));
            EndCircle.Add(new Vector3(-307.4183f, -1355.046f, 91.91059f));
            EndCircle.Add(new Vector3(-305.748f, -1355.202f, 91.79271f));
            EndCircle.Add(new Vector3(-303.9248f, -1354.959f, 91.67629f));
            EndCircle.Add(new Vector3(-302.1046f, -1354.198f, 91.39583f));
            EndCircle.Add(new Vector3(-300.6617f, -1353.176f, 91.30333f));
            EndCircle.Add(new Vector3(-299.4357f, -1351.891f, 91.17201f));
            EndCircle.Add(new Vector3(-298.3254f, -1350.521f, 91.103f));
            EndCircle.Add(new Vector3(-297.1552f, -1349.025f, 91.10437f));

            TopGYToEnd.Add(new Vector3(-371.2521f, 673.6852f, 29.78076f), WaypointAction.Mount);
            TopGYToEnd.Add(new Vector3(-369.0246f, 672.4365f, 29.78076f));
            TopGYToEnd.Add(new Vector3(-366.7971f, 671.1877f, 29.78076f));
            TopGYToEnd.Add(new Vector3(-364.4692f, 669.9125f, 29.68977f));
            TopGYToEnd.Add(new Vector3(-362.0844f, 668.6364f, 29.52429f));
            TopGYToEnd.Add(new Vector3(-360.0106f, 667.5268f, 29.29024f));
            TopGYToEnd.Add(new Vector3(-357.7121f, 666.6235f, 29.17031f));
            TopGYToEnd.Add(new Vector3(-355.2386f, 665.8672f, 29.20761f));
            TopGYToEnd.Add(new Vector3(-352.3488f, 665.2433f, 29.33608f));
            TopGYToEnd.Add(new Vector3(-350.3092f, 664.913f, 29.52775f));
            TopGYToEnd.Add(new Vector3(-346.8479f, 664.6492f, 29.7282f));
            TopGYToEnd.Add(new Vector3(-343.8941f, 664.5189f, 29.78343f));
            TopGYToEnd.Add(new Vector3(-338.2847f, 664.6537f, 29.90513f));
            TopGYToEnd.Add(new Vector3(-335.3625f, 664.7327f, 30.07569f));
            TopGYToEnd.Add(new Vector3(-332.5412f, 664.8091f, 30.29142f));
            TopGYToEnd.Add(new Vector3(-329.8542f, 664.8818f, 30.54919f));
            TopGYToEnd.Add(new Vector3(-327.3008f, 664.9121f, 30.9801f));
            TopGYToEnd.Add(new Vector3(-324.2276f, 664.8427f, 30.92558f));
            TopGYToEnd.Add(new Vector3(-321.1817f, 664.5801f, 30.7291f));
            TopGYToEnd.Add(new Vector3(-318.4929f, 664.1659f, 30.73509f));
            TopGYToEnd.Add(new Vector3(-316.2535f, 663.6354f, 30.80335f));
            TopGYToEnd.Add(new Vector3(-312.738f, 662.6736f, 30.27796f));
            TopGYToEnd.Add(new Vector3(-310.5272f, 661.8727f, 30.12764f));
            TopGYToEnd.Add(new Vector3(-307.1257f, 660.3464f, 30.21661f));
            TopGYToEnd.Add(new Vector3(-304.2853f, 658.8547f, 30.26796f));
            TopGYToEnd.Add(new Vector3(-301.9135f, 657.3899f, 30.29095f));
            TopGYToEnd.Add(new Vector3(-299.2853f, 655.3811f, 30.35842f));
            TopGYToEnd.Add(new Vector3(-297.1622f, 653.3982f, 30.39666f));
            TopGYToEnd.Add(new Vector3(-295.3142f, 651.054f, 30.34727f));
            TopGYToEnd.Add(new Vector3(-294.2156f, 647.9926f, 30.32732f));
            TopGYToEnd.Add(new Vector3(-294.8592f, 644.9795f, 30.34448f));
            TopGYToEnd.Add(new Vector3(-296.7241f, 642.5172f, 30.28588f));
            TopGYToEnd.Add(new Vector3(-298.9004f, 640.2802f, 30.19841f));
            TopGYToEnd.Add(new Vector3(-301.0758f, 638.3024f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-303.3091f, 636.3406f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-305.6707f, 634.5337f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-308.0621f, 632.7103f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-310.3465f, 630.9684f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-312.8715f, 629.0432f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-315.2762f, 627.2097f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-317.5206f, 625.4984f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-319.765f, 623.787f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-322.1163f, 621.9942f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-324.6546f, 620.0588f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-327.2062f, 618.1132f, 30.13361f));
            TopGYToEnd.Add(new Vector3(-329.7302f, 616.1887f, 30.15751f));
            TopGYToEnd.Add(new Vector3(-332.2017f, 614.3041f, 30.23452f));
            TopGYToEnd.Add(new Vector3(-334.7469f, 612.1255f, 30.24974f));
            TopGYToEnd.Add(new Vector3(-336.389f, 609.7094f, 30.24021f));
            TopGYToEnd.Add(new Vector3(-337.521f, 606.704f, 30.20775f));
            TopGYToEnd.Add(new Vector3(-337.8765f, 603.6156f, 30.24031f));
            TopGYToEnd.Add(new Vector3(-338.0956f, 600.2617f, 30.19132f));
            TopGYToEnd.Add(new Vector3(-338.0115f, 597.2067f, 30.14861f));
            TopGYToEnd.Add(new Vector3(-337.716f, 594.1973f, 30.13363f));
            TopGYToEnd.Add(new Vector3(-337.3085f, 591.2531f, 30.13359f));
            TopGYToEnd.Add(new Vector3(-336.7461f, 588.2307f, 30.13359f));
            TopGYToEnd.Add(new Vector3(-335.8774f, 585.2503f, 30.05224f));
            TopGYToEnd.Add(new Vector3(-334.8253f, 582.7952f, 29.78693f));
            TopGYToEnd.Add(new Vector3(-333.7021f, 580.2242f, 28.8483f));
            TopGYToEnd.Add(new Vector3(-332.3636f, 577.1606f, 27.43944f));
            TopGYToEnd.Add(new Vector3(-331.4056f, 574.8124f, 26.21666f));
            TopGYToEnd.Add(new Vector3(-330.334f, 571.6461f, 24.25808f));
            TopGYToEnd.Add(new Vector3(-329.5779f, 569.0143f, 22.56482f));
            TopGYToEnd.Add(new Vector3(-328.7436f, 565.7075f, 20.52661f));
            TopGYToEnd.Add(new Vector3(-327.9997f, 562.7072f, 18.81016f));
            TopGYToEnd.Add(new Vector3(-327.2841f, 559.821f, 17.18362f));
            TopGYToEnd.Add(new Vector3(-326.5352f, 556.7008f, 15.37993f));
            TopGYToEnd.Add(new Vector3(-325.9309f, 553.9783f, 13.7309f));
            TopGYToEnd.Add(new Vector3(-325.2462f, 550.6888f, 11.54571f));
            TopGYToEnd.Add(new Vector3(-324.6639f, 547.6359f, 9.521619f));
            TopGYToEnd.Add(new Vector3(-324.0981f, 544.4091f, 7.511708f));
            TopGYToEnd.Add(new Vector3(-323.6562f, 541.1296f, 5.831315f));
            TopGYToEnd.Add(new Vector3(-323.4551f, 537.5587f, 4.223535f));
            TopGYToEnd.Add(new Vector3(-323.6718f, 534.0396f, 2.8646f));
            TopGYToEnd.Add(new Vector3(-324.0732f, 530.7545f, 1.748807f));
            TopGYToEnd.Add(new Vector3(-324.5677f, 527.857f, 0.8877177f));
            TopGYToEnd.Add(new Vector3(-325.3899f, 524.5653f, -0.003913097f));
            TopGYToEnd.Add(new Vector3(-326.2069f, 521.8113f, -0.5965358f));
            TopGYToEnd.Add(new Vector3(-327.3302f, 518.8618f, -0.9443536f));
            TopGYToEnd.Add(new Vector3(-328.6751f, 515.967f, -1.084032f));
            TopGYToEnd.Add(new Vector3(-330.2726f, 513.1081f, -1.082011f));
            TopGYToEnd.Add(new Vector3(-332.0319f, 510.285f, -1.08095f));
            TopGYToEnd.Add(new Vector3(-334.1018f, 507.1228f, -1.084182f));
            TopGYToEnd.Add(new Vector3(-335.8408f, 504.6284f, -1.084182f));
            TopGYToEnd.Add(new Vector3(-337.6867f, 502.1071f, -1.084182f));
            TopGYToEnd.Add(new Vector3(-339.6536f, 499.4246f, -1.084182f));
            TopGYToEnd.Add(new Vector3(-341.2729f, 497.2162f, -1.176172f));
            TopGYToEnd.Add(new Vector3(-342.9616f, 494.913f, -1.243809f));
            TopGYToEnd.Add(new Vector3(-344.5809f, 492.7046f, -1.235715f));
            TopGYToEnd.Add(new Vector3(-346.4882f, 490.1033f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-348.2267f, 487.7324f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-349.8857f, 485.4698f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-351.5148f, 483.2479f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-353.4818f, 480.5654f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-355.3891f, 477.9641f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-357.0381f, 475.7151f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-358.8498f, 473.1492f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-360.5671f, 470.479f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-361.952f, 468.0971f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-363.3192f, 465.5718f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-364.6645f, 462.8823f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-365.882f, 460.4482f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-367.2875f, 457.6385f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-368.3698f, 455.4749f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-369.7527f, 452.7103f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-371.2709f, 449.6753f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-372.6538f, 446.9106f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-374.1344f, 443.9507f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-375.4354f, 441.333f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-376.6585f, 438.5491f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-377.7571f, 435.8589f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-378.8574f, 432.9162f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-379.8987f, 430.1309f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-381.0578f, 427.0309f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-382.2051f, 423.9624f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-383.2406f, 421.1928f, -1.243844f));
            TopGYToEnd.Add(new Vector3(-384.3996f, 418.0928f, -1.243804f));
            TopGYToEnd.Add(new Vector3(-385.4998f, 415.1501f, -1.243804f));
            TopGYToEnd.Add(new Vector3(-386.6471f, 412.0816f, -1.243804f));
            TopGYToEnd.Add(new Vector3(-387.7634f, 409.0912f, -1.237585f));
            TopGYToEnd.Add(new Vector3(-388.833f, 405.7474f, -1.243853f));
            TopGYToEnd.Add(new Vector3(-389.6343f, 402.4683f, -1.2409f));
            TopGYToEnd.Add(new Vector3(-390.3431f, 398.9951f, -1.242551f));
            TopGYToEnd.Add(new Vector3(-390.9705f, 395.8482f, -1.242551f));
            TopGYToEnd.Add(new Vector3(-391.5981f, 392.4965f, -1.242551f));
            TopGYToEnd.Add(new Vector3(-391.9889f, 389.7353f, -1.208067f));
            TopGYToEnd.Add(new Vector3(-392.4738f, 386.2407f, -1.16284f));
            TopGYToEnd.Add(new Vector3(-392.8755f, 383.3453f, -1.071057f));
            TopGYToEnd.Add(new Vector3(-393.0781f, 380.4497f, -1.080401f), WaypointAction.Stop);

            STToEnd.Add(new Vector3(-492.3517f, 74.13821f, 48.73773f), WaypointAction.Mount);
            STToEnd.Add(new Vector3(-490.9561f, 76.32697f, 48.84686f));
            STToEnd.Add(new Vector3(-489.5558f, 77.16782f, 48.91863f));
            STToEnd.Add(new Vector3(-488.0407f, 77.56642f, 48.97254f));
            STToEnd.Add(new Vector3(-486.6588f, 77.64903f, 49.00435f));
            STToEnd.Add(new Vector3(-485.3479f, 77.70685f, 49.04086f));
            STToEnd.Add(new Vector3(-483.8751f, 77.67712f, 49.02653f));
            STToEnd.Add(new Vector3(-482.2656f, 77.63847f, 48.95288f));
            STToEnd.Add(new Vector3(-480.8653f, 77.60485f, 48.93704f));
            STToEnd.Add(new Vector3(-479.3339f, 77.48319f, 48.90607f));
            STToEnd.Add(new Vector3(-477.9423f, 77.3234f, 48.86304f));
            STToEnd.Add(new Vector3(-476.2469f, 77.12872f, 48.84251f));
            STToEnd.Add(new Vector3(-474.5674f, 76.93587f, 48.83067f));
            STToEnd.Add(new Vector3(-472.9199f, 76.7467f, 48.78032f));
            STToEnd.Add(new Vector3(-471.2005f, 76.54926f, 48.78511f));
            STToEnd.Add(new Vector3(-469.625f, 76.36835f, 48.70339f));
            STToEnd.Add(new Vector3(-467.9456f, 76.17444f, 48.68389f));
            STToEnd.Add(new Vector3(-466.4515f, 75.99097f, 48.64849f));
            STToEnd.Add(new Vector3(-464.809f, 75.8331f, 48.56902f));
            STToEnd.Add(new Vector3(-463.1479f, 75.70163f, 48.49952f));
            STToEnd.Add(new Vector3(-461.567f, 75.5765f, 48.42747f));
            STToEnd.Add(new Vector3(-460.0823f, 75.459f, 48.35414f));
            STToEnd.Add(new Vector3(-458.4212f, 75.32752f, 48.28119f));
            STToEnd.Add(new Vector3(-456.8002f, 75.19923f, 48.22807f));
            STToEnd.Add(new Vector3(-455.139f, 75.06775f, 48.17324f));
            STToEnd.Add(new Vector3(-453.4377f, 74.9331f, 48.22797f));
            STToEnd.Add(new Vector3(-451.3673f, 74.76923f, 48.47121f));
            STToEnd.Add(new Vector3(-449.8987f, 74.65299f, 48.65379f));
            STToEnd.Add(new Vector3(-448.1493f, 74.51453f, 48.78106f));
            STToEnd.Add(new Vector3(-446.2314f, 74.36273f, 48.93809f));
            STToEnd.Add(new Vector3(-444.3936f, 74.21729f, 48.97009f));
            STToEnd.Add(new Vector3(-442.7245f, 74.08517f, 49.02585f));
            STToEnd.Add(new Vector3(-440.9429f, 73.94417f, 49.0285f));
            STToEnd.Add(new Vector3(-439.1935f, 73.80571f, 49.0409f));
            STToEnd.Add(new Vector3(-437.5404f, 73.67487f, 49.05184f));
            STToEnd.Add(new Vector3(-435.8231f, 73.53895f, 49.02435f));
            STToEnd.Add(new Vector3(-433.9678f, 73.41613f, 49.03026f));
            STToEnd.Add(new Vector3(-432.0797f, 73.49792f, 48.97166f));
            STToEnd.Add(new Vector3(-430.3787f, 73.80768f, 49.09697f));
            STToEnd.Add(new Vector3(-428.6656f, 74.34145f, 49.30193f));
            STToEnd.Add(new Vector3(-427.1062f, 74.88143f, 49.43285f));
            STToEnd.Add(new Vector3(-425.3631f, 75.57171f, 49.64745f));
            STToEnd.Add(new Vector3(-423.9568f, 76.173f, 49.77457f));
            STToEnd.Add(new Vector3(-422.2914f, 76.88507f, 49.90829f));
            STToEnd.Add(new Vector3(-420.6334f, 77.59397f, 49.98284f));
            STToEnd.Add(new Vector3(-419.2492f, 78.18578f, 49.97741f));
            STToEnd.Add(new Vector3(-417.8947f, 78.76492f, 49.93728f));
            STToEnd.Add(new Vector3(-416.1331f, 79.51813f, 49.83015f));
            STToEnd.Add(new Vector3(-414.5713f, 80.18588f, 49.62507f));
            STToEnd.Add(new Vector3(-412.8911f, 80.90427f, 49.31929f));
            STToEnd.Add(new Vector3(-411.3219f, 81.5752f, 49.01685f));
            STToEnd.Add(new Vector3(-409.7909f, 82.27128f, 48.72591f));
            STToEnd.Add(new Vector3(-408.312f, 83.15315f, 48.46766f));
            STToEnd.Add(new Vector3(-407.011f, 84.08701f, 48.2456f));
            STToEnd.Add(new Vector3(-405.7371f, 85.19789f, 48.02953f));
            STToEnd.Add(new Vector3(-404.4479f, 86.38855f, 47.72036f));
            STToEnd.Add(new Vector3(-403.2356f, 87.5082f, 47.3863f));
            STToEnd.Add(new Vector3(-402.106f, 88.55139f, 46.98697f));
            STToEnd.Add(new Vector3(-400.6927f, 89.85675f, 46.44823f));
            STToEnd.Add(new Vector3(-399.64f, 90.82893f, 46.07484f));
            STToEnd.Add(new Vector3(-398.1852f, 92.17252f, 45.78608f));
            STToEnd.Add(new Vector3(-397.0321f, 93.23756f, 45.60543f));
            STToEnd.Add(new Vector3(-395.7725f, 94.40091f, 45.43798f));
            STToEnd.Add(new Vector3(-394.4833f, 95.59157f, 45.37933f));
            STToEnd.Add(new Vector3(-393.336f, 96.65114f, 45.28218f));
            STToEnd.Add(new Vector3(-392.0882f, 97.80357f, 45.1693f));
            STToEnd.Add(new Vector3(-390.7931f, 98.99968f, 45.11757f));
            STToEnd.Add(new Vector3(-389.5512f, 100.1466f, 45.11108f));
            STToEnd.Add(new Vector3(-388.0432f, 101.5394f, 45.09604f));
            STToEnd.Add(new Vector3(-386.8664f, 102.6263f, 45.09452f));
            STToEnd.Add(new Vector3(-385.5417f, 103.8497f, 45.09452f));
            STToEnd.Add(new Vector3(-384.2644f, 105.0294f, 45.08759f));
            STToEnd.Add(new Vector3(-382.8273f, 106.3566f, 44.98781f));
            STToEnd.Add(new Vector3(-381.5677f, 107.52f, 44.93863f));
            STToEnd.Add(new Vector3(-380.314f, 108.6779f, 44.82733f));
            STToEnd.Add(new Vector3(-379.0248f, 109.8685f, 44.71748f));
            STToEnd.Add(new Vector3(-377.7948f, 111.0046f, 44.64969f));
            STToEnd.Add(new Vector3(-376.5529f, 112.1515f, 44.47953f));
            STToEnd.Add(new Vector3(-375.311f, 113.2985f, 44.27607f));
            STToEnd.Add(new Vector3(-374.1696f, 114.3526f, 44.07059f));
            STToEnd.Add(new Vector3(-372.8568f, 115.5651f, 43.83585f));
            STToEnd.Add(new Vector3(-371.544f, 116.7776f, 43.61529f));
            STToEnd.Add(new Vector3(-370.3376f, 117.8918f, 43.47099f));
            STToEnd.Add(new Vector3(-368.9242f, 119.1972f, 43.31257f), WaypointAction.Stop);

            IBToEnd.Add(new Vector3(-402.2801f, -531.6005f, 49.54607f), WaypointAction.Mount);
            IBToEnd.Add(new Vector3(-400.8354f, -531.6403f, 49.59376f));
            IBToEnd.Add(new Vector3(-399.204f, -532.3002f, 49.67522f));
            IBToEnd.Add(new Vector3(-397.8615f, -533.5037f, 49.73354f));
            IBToEnd.Add(new Vector3(-396.9379f, -534.7527f, 49.73604f));
            IBToEnd.Add(new Vector3(-396.1206f, -536.3918f, 49.84799f));
            IBToEnd.Add(new Vector3(-395.4413f, -537.9837f, 49.90145f));
            IBToEnd.Add(new Vector3(-394.6799f, -539.7681f, 49.9327f));
            IBToEnd.Add(new Vector3(-394.0101f, -541.3378f, 49.94385f));
            IBToEnd.Add(new Vector3(-393.4207f, -542.8444f, 49.91052f));
            IBToEnd.Add(new Vector3(-392.8732f, -544.3583f, 49.91721f));
            IBToEnd.Add(new Vector3(-392.3034f, -546.1282f, 50.02924f));
            IBToEnd.Add(new Vector3(-391.9616f, -547.8077f, 50.0272f));
            IBToEnd.Add(new Vector3(-391.7711f, -549.5594f, 49.98869f));
            IBToEnd.Add(new Vector3(-391.7691f, -551.209f, 50.22095f));
            IBToEnd.Add(new Vector3(-392.0089f, -553.2366f, 50.66891f));
            IBToEnd.Add(new Vector3(-392.4987f, -555.2619f, 51.17091f));
            IBToEnd.Add(new Vector3(-393.0534f, -556.9456f, 51.55956f));
            IBToEnd.Add(new Vector3(-393.8004f, -558.6567f, 51.62295f));
            IBToEnd.Add(new Vector3(-394.8616f, -560.5263f, 51.76116f));
            IBToEnd.Add(new Vector3(-395.948f, -562.0748f, 51.84794f));
            IBToEnd.Add(new Vector3(-397.1891f, -563.7396f, 51.96272f));
            IBToEnd.Add(new Vector3(-398.2416f, -565.0933f, 52.08396f));
            IBToEnd.Add(new Vector3(-399.2101f, -566.3389f, 52.22106f));
            IBToEnd.Add(new Vector3(-400.3317f, -567.7815f, 52.45446f));
            IBToEnd.Add(new Vector3(-401.2706f, -568.989f, 52.65835f));
            IBToEnd.Add(new Vector3(-402.3823f, -570.4188f, 52.771f));
            IBToEnd.Add(new Vector3(-403.2915f, -571.5882f, 52.87552f));
            IBToEnd.Add(new Vector3(-404.4033f, -573.0181f, 53.06418f));
            IBToEnd.Add(new Vector3(-405.4755f, -574.3972f, 53.2253f));
            IBToEnd.Add(new Vector3(-406.4292f, -575.6237f, 53.41134f));
            IBToEnd.Add(new Vector3(-407.531f, -577.0408f, 53.65192f));
            IBToEnd.Add(new Vector3(-408.544f, -578.3436f, 53.87808f));
            IBToEnd.Add(new Vector3(-409.5567f, -579.6765f, 54.17955f));
            IBToEnd.Add(new Vector3(-410.388f, -581.2022f, 54.4917f));
            IBToEnd.Add(new Vector3(-410.9303f, -582.6561f, 54.82674f));
            IBToEnd.Add(new Vector3(-411.2875f, -584.1758f, 55.09409f));
            IBToEnd.Add(new Vector3(-411.5585f, -585.8441f, 55.35683f));
            IBToEnd.Add(new Vector3(-411.6848f, -587.5538f, 55.69045f));
            IBToEnd.Add(new Vector3(-411.7269f, -589.2839f, 55.99171f));
            IBToEnd.Add(new Vector3(-411.6894f, -590.9818f, 56.32224f));
            IBToEnd.Add(new Vector3(-411.5573f, -592.6025f, 56.72326f));
            IBToEnd.Add(new Vector3(-411.412f, -594.3029f, 57.12917f));
            IBToEnd.Add(new Vector3(-411.2681f, -595.9872f, 57.5874f));
            IBToEnd.Add(new Vector3(-411.1235f, -597.6796f, 57.6935f));
            IBToEnd.Add(new Vector3(-410.9514f, -599.4742f, 58.2923f));
            IBToEnd.Add(new Vector3(-410.531f, -601.0521f, 58.83203f));
            IBToEnd.Add(new Vector3(-410.0436f, -602.6653f, 59.30796f));
            IBToEnd.Add(new Vector3(-409.6036f, -604.1218f, 59.7786f));
            IBToEnd.Add(new Vector3(-409.0355f, -605.8839f, 59.91433f));
            IBToEnd.Add(new Vector3(-408.4634f, -607.4659f, 59.9763f));
            IBToEnd.Add(new Vector3(-407.9102f, -608.8746f, 60.00261f));
            IBToEnd.Add(new Vector3(-407.2704f, -610.4131f, 59.98314f));
            IBToEnd.Add(new Vector3(-406.5284f, -612.127f, 59.87058f));
            IBToEnd.Add(new Vector3(-405.8591f, -613.6618f, 59.7053f));
            IBToEnd.Add(new Vector3(-405.1094f, -615.3811f, 59.41878f));
            IBToEnd.Add(new Vector3(-404.4112f, -616.9824f, 59.19595f));
            IBToEnd.Add(new Vector3(-403.7419f, -618.5172f, 59.17294f));
            IBToEnd.Add(new Vector3(-402.9826f, -620.2586f, 59.17945f));
            IBToEnd.Add(new Vector3(-402.1975f, -622.0591f, 59.29744f));
            IBToEnd.Add(new Vector3(-401.4703f, -623.7267f, 59.30445f));
            IBToEnd.Add(new Vector3(-400.6818f, -625.5345f, 59.22713f));
            IBToEnd.Add(new Vector3(-399.9387f, -627.0704f, 59.16492f));
            IBToEnd.Add(new Vector3(-399.1338f, -628.4924f, 59.11127f));
            IBToEnd.Add(new Vector3(-398.1443f, -630.133f, 59.1353f));
            IBToEnd.Add(new Vector3(-397.2752f, -631.5548f, 59.18122f));
            IBToEnd.Add(new Vector3(-396.2676f, -633.2032f, 59.33168f));
            IBToEnd.Add(new Vector3(-395.4238f, -634.5839f, 59.41131f));
            IBToEnd.Add(new Vector3(-394.2189f, -636.5551f, 59.57409f), WaypointAction.Stop);

            SFToEnd.Add(new Vector3(38.73853f, -165.3444f, 77.21858f), WaypointAction.Mount);
            SFToEnd.Add(new Vector3(40.45637f, -168.0801f, 77.24858f));
            SFToEnd.Add(new Vector3(41.0021f, -171.0889f, 77.2116f));
            SFToEnd.Add(new Vector3(40.78061f, -173.9049f, 77.14484f));
            SFToEnd.Add(new Vector3(39.73492f, -176.4096f, 77.09158f));
            SFToEnd.Add(new Vector3(37.69539f, -178.6065f, 77.07859f));
            SFToEnd.Add(new Vector3(34.57934f, -179.2259f, 77.15762f));
            SFToEnd.Add(new Vector3(31.4068f, -179.5972f, 77.37116f));
            SFToEnd.Add(new Vector3(28.31133f, -180.1173f, 77.80399f));
            SFToEnd.Add(new Vector3(25.17349f, -179.9856f, 78.36815f));
            SFToEnd.Add(new Vector3(22.3693f, -179.6656f, 78.80653f));
            SFToEnd.Add(new Vector3(18.89649f, -178.974f, 79.12082f));
            SFToEnd.Add(new Vector3(15.49348f, -178.1801f, 79.27223f));
            SFToEnd.Add(new Vector3(11.57603f, -177.4747f, 79.36818f));
            SFToEnd.Add(new Vector3(8.295878f, -177.0414f, 79.17828f));
            SFToEnd.Add(new Vector3(4.798318f, -177.4999f, 79.56715f));
            SFToEnd.Add(new Vector3(1.598593f, -178.3456f, 80.04704f));
            SFToEnd.Add(new Vector3(-1.769621f, -179.2231f, 80.46022f));
            SFToEnd.Add(new Vector3(-4.84257f, -180.0224f, 80.70166f));
            SFToEnd.Add(new Vector3(-7.964296f, -180.8343f, 80.77291f));
            SFToEnd.Add(new Vector3(-11.00473f, -181.6252f, 80.77291f));
            SFToEnd.Add(new Vector3(-14.70903f, -182.5998f, 80.77291f));
            SFToEnd.Add(new Vector3(-18.00765f, -183.5286f, 80.73972f));
            SFToEnd.Add(new Vector3(-21.26511f, -184.5384f, 80.56522f));
            SFToEnd.Add(new Vector3(-24.23283f, -185.4616f, 79.94879f));
            SFToEnd.Add(new Vector3(-27.48931f, -186.4747f, 78.52467f));
            SFToEnd.Add(new Vector3(-30.4089f, -187.3829f, 77.3475f));
            SFToEnd.Add(new Vector3(-33.53704f, -188.356f, 76.27696f));
            SFToEnd.Add(new Vector3(-36.85767f, -189.389f, 75.27932f));
            SFToEnd.Add(new Vector3(-40.35477f, -190.4769f, 74.3825f));
            SFToEnd.Add(new Vector3(-43.35458f, -191.41f, 73.75964f));
            SFToEnd.Add(new Vector3(-46.37042f, -192.3482f, 73.27028f));
            SFToEnd.Add(new Vector3(-49.53036f, -193.3322f, 72.84185f));
            SFToEnd.Add(new Vector3(-52.42136f, -194.2748f, 72.59982f));
            SFToEnd.Add(new Vector3(-55.96416f, -195.4403f, 72.43505f));
            SFToEnd.Add(new Vector3(-57.90636f, -196.095f, 72.39445f));
            SFToEnd.Add(new Vector3(-61.31129f, -197.2491f, 72.48525f));
            SFToEnd.Add(new Vector3(-64.14123f, -198.2155f, 72.63902f));
            SFToEnd.Add(new Vector3(-67.25734f, -199.2797f, 72.96858f));
            SFToEnd.Add(new Vector3(-70.55385f, -200.4054f, 73.45397f));
            SFToEnd.Add(new Vector3(-73.71765f, -201.4858f, 74.02583f));
            SFToEnd.Add(new Vector3(-77.02285f, -202.6201f, 74.80865f));
            SFToEnd.Add(new Vector3(-79.98729f, -203.66f, 75.65211f));
            SFToEnd.Add(new Vector3(-83.35509f, -204.8695f, 76.74198f));
            SFToEnd.Add(new Vector3(-86.36995f, -205.9681f, 77.88459f));
            SFToEnd.Add(new Vector3(-89.55058f, -207.1512f, 79.28064f));
            SFToEnd.Add(new Vector3(-92.20877f, -208.3242f, 80.29132f));
            SFToEnd.Add(new Vector3(-95.10629f, -210.5752f, 80.41759f));
            SFToEnd.Add(new Vector3(-96.91807f, -213.4794f, 80.12573f));
            SFToEnd.Add(new Vector3(-97.49155f, -216.6172f, 79.90166f));
            SFToEnd.Add(new Vector3(-96.81269f, -219.8418f, 78.786f));
            SFToEnd.Add(new Vector3(-95.34705f, -222.7996f, 77.43337f));
            SFToEnd.Add(new Vector3(-93.59811f, -225.4698f, 75.71287f));
            SFToEnd.Add(new Vector3(-91.84774f, -228.1391f, 73.48002f));
            SFToEnd.Add(new Vector3(-89.99603f, -230.9629f, 70.82986f));
            SFToEnd.Add(new Vector3(-88.0985f, -233.9363f, 67.952f));
            SFToEnd.Add(new Vector3(-86.57561f, -237.1352f, 64.97916f));
            SFToEnd.Add(new Vector3(-85.49223f, -239.8681f, 63.00044f));
            SFToEnd.Add(new Vector3(-84.33864f, -243.3076f, 61.2319f));
            SFToEnd.Add(new Vector3(-83.39473f, -246.8284f, 59.42572f));
            SFToEnd.Add(new Vector3(-82.83855f, -250.732f, 57.62172f));
            SFToEnd.Add(new Vector3(-83.09702f, -253.8342f, 56.20149f));
            SFToEnd.Add(new Vector3(-84.43633f, -256.9546f, 54.74658f));
            SFToEnd.Add(new Vector3(-86.62496f, -259.9731f, 52.4374f));
            SFToEnd.Add(new Vector3(-88.79335f, -262.4727f, 50.05245f));
            SFToEnd.Add(new Vector3(-90.93787f, -264.8821f, 47.91761f));
            SFToEnd.Add(new Vector3(-93.11589f, -267.3293f, 45.68162f));
            SFToEnd.Add(new Vector3(-95.39444f, -269.8893f, 43.27166f));
            SFToEnd.Add(new Vector3(-97.5948f, -272.3615f, 41.03636f));
            SFToEnd.Add(new Vector3(-99.94036f, -274.9969f, 38.5262f));
            SFToEnd.Add(new Vector3(-101.8838f, -277.1805f, 36.01503f));
            SFToEnd.Add(new Vector3(-103.4593f, -279.0811f, 34.03364f));
            SFToEnd.Add(new Vector3(-105.3636f, -281.4734f, 31.99814f));
            SFToEnd.Add(new Vector3(-107.8834f, -284.5771f, 29.2827f));
            SFToEnd.Add(new Vector3(-110.181f, -286.888f, 26.85486f));
            SFToEnd.Add(new Vector3(-113.2213f, -289.6364f, 24.06476f));
            SFToEnd.Add(new Vector3(-115.7182f, -291.8342f, 21.59809f));
            SFToEnd.Add(new Vector3(-118.6347f, -294.3943f, 18.9325f));
            SFToEnd.Add(new Vector3(-121.3921f, -296.1233f, 17.26115f));
            SFToEnd.Add(new Vector3(-124.4006f, -297.8676f, 16.16889f));
            SFToEnd.Add(new Vector3(-127.3365f, -299.5698f, 15.36624f));
            SFToEnd.Add(new Vector3(-130.127f, -301.1877f, 14.86908f));
            SFToEnd.Add(new Vector3(-132.932f, -302.814f, 14.4303f));
            SFToEnd.Add(new Vector3(-135.8969f, -304.5331f, 13.87928f));
            SFToEnd.Add(new Vector3(-139.0943f, -306.3869f, 13.25477f));
            SFToEnd.Add(new Vector3(-142.1755f, -308.1733f, 12.60629f));
            SFToEnd.Add(new Vector3(-144.9369f, -309.7744f, 11.97034f));
            SFToEnd.Add(new Vector3(-147.6798f, -311.5889f, 11.35529f));
            SFToEnd.Add(new Vector3(-150.0491f, -313.65f, 10.82002f));
            SFToEnd.Add(new Vector3(-152.6163f, -316.5339f, 10.28244f));
            SFToEnd.Add(new Vector3(-154.5365f, -318.87f, 9.8648f));
            SFToEnd.Add(new Vector3(-156.9368f, -321.7901f, 9.48492f));
            SFToEnd.Add(new Vector3(-159.0598f, -324.3727f, 9.294856f));
            SFToEnd.Add(new Vector3(-161.1934f, -326.9684f, 9.259957f));
            SFToEnd.Add(new Vector3(-163.2097f, -329.4212f, 9.259957f));
            SFToEnd.Add(new Vector3(-165.418f, -332.1077f, 9.259957f));
            SFToEnd.Add(new Vector3(-167.6796f, -334.8591f, 9.259957f));
            SFToEnd.Add(new Vector3(-169.8448f, -337.5368f, 9.259957f));
            SFToEnd.Add(new Vector3(-171.8987f, -340.4464f, 9.263782f));
            SFToEnd.Add(new Vector3(-173.841f, -343.5279f, 9.291479f));
            SFToEnd.Add(new Vector3(-175.1734f, -346.4802f, 9.375202f));
            SFToEnd.Add(new Vector3(-176.4901f, -349.9516f, 9.64011f));
            SFToEnd.Add(new Vector3(-177.6503f, -353.0512f, 10.30009f));
            SFToEnd.Add(new Vector3(-178.8163f, -356.1665f, 11.03963f));
            SFToEnd.Add(new Vector3(-179.9941f, -359.3133f, 11.73874f));
            SFToEnd.Add(new Vector3(-180.8194f, -362.3776f, 12.38877f));
            SFToEnd.Add(new Vector3(-180.855f, -366.9634f, 13.26573f));
            SFToEnd.Add(new Vector3(-180.793f, -369.3984f, 13.60828f));
            SFToEnd.Add(new Vector3(-180.9268f, -372.9342f, 14.13808f));
            SFToEnd.Add(new Vector3(-181.3383f, -376.3874f, 14.4826f));
            SFToEnd.Add(new Vector3(-181.7888f, -379.7603f, 14.87469f));
            SFToEnd.Add(new Vector3(-182.659f, -383.2276f, 15.16725f));
            SFToEnd.Add(new Vector3(-183.9111f, -386.6292f, 15.37643f));
            SFToEnd.Add(new Vector3(-185.3898f, -389.7023f, 15.59641f));
            SFToEnd.Add(new Vector3(-186.8321f, -392.6998f, 16.1266f));
            SFToEnd.Add(new Vector3(-188.471f, -396.106f, 17.31831f));
            SFToEnd.Add(new Vector3(-189.7749f, -398.8158f, 18.77752f));
            SFToEnd.Add(new Vector3(-191.4284f, -402.2523f, 21.06155f));
            SFToEnd.Add(new Vector3(-193.0091f, -405.5374f, 22.68287f));
            SFToEnd.Add(new Vector3(-194.3494f, -408.3229f, 24.64769f));
            SFToEnd.Add(new Vector3(-195.9373f, -411.6231f, 26.04351f));
            SFToEnd.Add(new Vector3(-197.2995f, -414.4541f, 26.75396f));
            SFToEnd.Add(new Vector3(-198.9891f, -417.9664f, 27.03867f));
            SFToEnd.Add(new Vector3(-200.259f, -421.0212f, 26.8636f));
            SFToEnd.Add(new Vector3(-200.8813f, -424.7072f, 26.23873f), WaypointAction.Stop);

           

        }

        public void onKeyEvent(Object o, int key)
        {

            if (key == 0x75) // F6
            {
                theform.Enable();
                Enabledcheck.Checked = true;
                theform.moveclick();
                wasmoving = true;

                //                 WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                //                 if (plyr != null)
                //                 {
                //                     currentwaypoint = waypoints.FindClosestTo(new Vector3(plyr.X, plyr.Y, plyr.Z));
                //                 }

            }
            if (key == 0x76) // F7
            {
                Enabledcheck.Checked = false;
                theform.Disable();
                theform.moveunclick();
                wasmoving = false;
            }
            if (key == 0x77) // F8
            {
                WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                if (plyr != null)
                {
                    theform.AddWaypointText("waypoints.Add(new Vector3(" + plyr.X.ToString() + "f, " + plyr.Y.ToString() + "f, " + plyr.Z.ToString() + "f));");
                }

            }
        }
        public override string GetName()
        {
            return "WSGMovementBot";
        }
        public override void SetupMenu(MenuItem mbase)
        {
            Enabledcheck = new MenuItem("Enable", new System.EventHandler(popup));
            Enabledcheck.Checked = false;

            mbase.MenuItems.Add(Enabledcheck);
        }
        private void popup(object sender, EventArgs e)
        {
            MenuItem miClicked = (MenuItem)sender;
            string item = miClicked.Text;
            if (item == "Enable")
            {
                if (Enabledcheck.Checked == true)
                    Enabledcheck.Checked = false;
                else
                    Enabledcheck.Checked = true;
            }
        }
        WoahObject targ = null;
        Random r = new Random();

        uint count = 0;
        bool reverse = false;
        bool wasdead = false;
        bool wasnotinbg = false;
        bool wasfighting = false;
        bool wasmoving = false;
        bool needQ = true;
        string prevloc = "";
        Vector3 targetpos;
        int jumpcount = 0;
        int failedcount = 0;
        public override void DoAction()
        {
            while (true)
            {

                if (Enabledcheck.Checked == false)
                {
                    wasmoving = false;
                    Thread.Sleep(33);
                    continue;
                }
                try
                {
                    env.Update();

                    if (env.Location != "battleground" || env.Location == null)
                    {
                        waypoints = StartToEnd;
                        WoahObject plyar = (WoahObject)env.Objects[env.PlayerID];
                        if (plyar != null)
                        {
                            currentwaypoint = waypoints.FindClosestTo(new Vector3(plyar.X, plyar.Y, 0));
                        }
                        if (wasmoving == true)
                        {
                            theform.moveunclick();
                            wasmoving = false;
                        }

                        if (needQ)
                        {
                            theform.joinAV();
                            needQ = false;
                        }

                        jumpcount += 1;
                        if (jumpcount % 2000 == 0)
                        {
                            ArrayList input = new ArrayList();
                            input.Add(new keyinput(0x39, true));// SPACE
                            input.Add(new keyinput(0x39, false));
                            theform.sendline(input);
                        }

                        wasnotinbg = true;
                        Thread.Sleep(66);
                        continue;
                    }
                    else if (env.Location == "battleground")
                    {

                        if (theform.joinblocking)
                        {
                            Thread.Sleep(66);
                            continue;
                        }
                        // For next time, when env.Location != battleground
                        needQ = true;

                        if (wasnotinbg == true)
                        {

                            wasnotinbg = false;
                            if (wasmoving == false)
                            {
                                wasmoving = true;
                                theform.moveclick();



                            }
                            waypoints = StartToEnd;
                            WoahObject plyar = (WoahObject)env.Objects[env.PlayerID];
                            if (plyar != null)
                            {

                                currentwaypoint = waypoints.FindClosestTo(new Vector3(plyar.X, plyar.Y, 0));
                                targetpos = currentwaypoint.location;
                            }
                        }
                    }


                    //                     if (prevloc != "" && prevloc != "battleground" && env.Location == "battleground")
                    //                     {
                    //                         waypoints = StartToSt;
                    //                         currentwaypoint = waypoints.Get(0);
                    //                         targetpos = currentwaypoint.location;
                    //                     }
                    //                     prevloc = env.Location;
                    //                    


                    WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                    if (plyr != null && plyr.GUID.High != 0)
                    {
                        failedcount = 0;
                        //theform.moveclick();
                        ++count;



                        // determine forward vector
                        float xx = (float)Math.Cos(plyr.Facing + (Math.PI / 2));
                        float yy = (float)Math.Sin(plyr.Facing + (Math.PI / 2));
                        Vector3 forward = new Vector3(xx, yy, 0);
                        Vector3 mypos = new Vector3(plyr.X, plyr.Y, 0);

                        if (Vector3.Distance(targetpos, new Vector3(0, 0, 0)) < 40)
                        {
                            mypos = new Vector3(plyr.X, plyr.Y, 0);
                            currentwaypoint = waypoints.FindClosestTo(mypos);
                            targetpos = currentwaypoint.location;
                        }

                        if (plyr.IsValid && plyr.Unit_IsDead && plyr.Unit_Pet == null)
                        {
                            theform.moveunclick();
                            wasdead = true;
                            Thread.Sleep(33);
                            continue;
                        }

                        // Attempt to fight people
                        // press tab to target an enemy
                        bool wefight = false;

                        if (count % 100 == 0)
                        {
                            if (plyr.Unit_Target == null)
                            {
                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x0F, true));// TAB
                                input.Add(new keyinput(0x0F, false));
                                theform.sendline(input);
                            }
                        }



                        if (plyr.Unit_Target != null && plyr.Unit_Target != plyr && plyr.Unit_Target != plyr.Unit_Pet)
                        {
                            // We have an enemy to fight
                            targetpos = new Vector3(plyr.Unit_Target.X, plyr.Unit_Target.Y, 0);
                            wefight = true;
                            wasfighting = true;
                        }
                        else if (plyr.Unit_Target != null && plyr.Unit_Target.Unit_Health == 0)
                        {
                            // press f1 to get off the corpse
                            ArrayList input = new ArrayList();
                            input.Add(new keyinput(0x3B, true));// F1
                            input.Add(new keyinput(0x3B, false));
                            theform.sendline(input);

                            // Find the closest waypoint of our list, and run there
                            mypos = new Vector3(plyr.X, plyr.Y, 0);
                            currentwaypoint = waypoints.FindClosestTo(mypos);
                            targetpos = currentwaypoint.location;
                        }
                        else if (wasdead)
                        {
                            // DETERMINE WHICH GRAVEYARD WE SPAWNED AT
                            theform.moveclick();
                            wasdead = false;
                            if (Vector3.Distance(mypos, RHToVann.Get(0).location) < 100)
                            {
                                waypoints = RHToVann;
                            }
                            else if (Vector3.Distance(mypos, StartToEnd.Get(0).location) < 100)
                            {
                                waypoints = StartToEnd;
                            }
                            else if (Vector3.Distance(mypos, TopGYToEnd.Get(0).location) < 100)
                            {
                                waypoints = TopGYToEnd;
                            }
                            else if (Vector3.Distance(mypos, STToEnd.Get(0).location) < 100)
                            {
                                waypoints = STToEnd;
                            }
                            else if (Vector3.Distance(mypos, IBToEnd.Get(0).location) < 100)
                            {
                                waypoints = IBToEnd;
                            }
                            else if (Vector3.Distance(mypos, SFToEnd.Get(0).location) < 100)
                            {
                                waypoints = SFToEnd;
                            } 
                            
                            mypos = new Vector3(plyr.X, plyr.Y, 0);
                            currentwaypoint = waypoints.FindClosestTo(mypos);
                            targetpos = currentwaypoint.location;
                        }
                        else
                        {
                            if (wasfighting)
                            {
                                wasfighting = false;
                                // Find the closest waypoint of our list, and run there
                                mypos = new Vector3(plyr.X, plyr.Y, 0);
                                currentwaypoint = waypoints.FindClosestTo(mypos);
                                targetpos = currentwaypoint.location;
                            }
                            else
                                targetpos = currentwaypoint.location;
                        }
                        targetpos.Z = 0;
#if DEBUG
                        if (count % 10 == 0)
                        {
                            theform.AddMovementText(waypoints.Name + ": " + currentwaypoint.ID.ToString() + ": " + targetpos.ToString() + "  my pos: " + mypos.ToString());
                        }
#endif
                        if (currentwaypoint.DoAction == WaypointAction.Jump)
                        {
                            ArrayList input = new ArrayList();
                            input.Add(new keyinput(0x39, true));// SPACE
                            input.Add(new keyinput(0x39, false));
                            theform.sendline(input);
                        }



                        Vector3 offset = targetpos - mypos;
                        float distance = (float)offset.Magnitude;

                        if (distance < 8f && wefight == false)
                        {
                            if (currentwaypoint.DoAction == WaypointAction.Mount)
                            {
                                theform.moveunclick();
                                Thread.Sleep(700);
                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x0D, true));// =
                                input.Add(new keyinput(0x0D, false));
                                theform.sendline(input);
                                Thread.Sleep(4500);
                                theform.moveclick();

                            }


                            if (currentwaypoint.DoAction == WaypointAction.Stop)
                            {
                                if (waypoints.Name == TopGYToEnd.Name)
                                {
                                    waypoints = StartToEnd;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == STToEnd.Name)
                                {
                                    waypoints = StartToEnd;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == IBToEnd.Name)
                                {
                                    waypoints = StartToEnd;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == SFToEnd.Name)
                                {
                                    waypoints = StartToEnd;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else
                                {
                                    waypoints = EndCircle;
                                    currentwaypoint = waypoints.Get(0);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;
                                }

                            }

                            bool wrapped;
                            currentwaypoint = waypoints.GetNext(currentwaypoint.ID, out wrapped);

                            Thread.Sleep(33);
                            continue;
                        }

                        float bias = -(float)Math.PI;
                        float xtemp = (float)(Math.Cos(plyr.Facing - bias) * offset.X - Math.Sin(plyr.Facing - bias) * offset.Y);
                        offset.Y = -(float)(Math.Sin(plyr.Facing - bias) * offset.X + Math.Cos(plyr.Facing - bias) * offset.Y);
                        offset.X = xtemp;

                        offset.Normalize();

                        float pre = plyr.Facing;

                        int gain = 4;
                        if (wefight)
                            gain = 8;

                        if (offset.X == 0 && offset.Y <= 0)
                        {
                            // Avoid running directly away
                            Point oldpos = Cursor.Position;
                            Cursor.Position = new Point(oldpos.X + gain * (int)(0.4 * 10), oldpos.Y);
                        }
                        else
                        {
                            Point oldpos = Cursor.Position;
                            Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
                        }
                    }
                    else
                    {
                        failedcount++;
                        if (failedcount > 3)
                        {
                            theform.AddIRCText("FAILED!");
                            theform.ReSetup();
                        }

                    }

                }
                catch (System.Exception ex)
                {

                }
                Thread.Sleep(16);
            }
        }

        public bool IsAhead(Vector3 me, Vector3 target, Vector3 forward, float cosThreshold)
        {
            Vector3 targetDirection = (target - me);
            targetDirection.Normalize();
            return Vector3.DotProduct(forward, targetDirection) > cosThreshold;
        }
    }
}
