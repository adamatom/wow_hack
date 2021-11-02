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
    class ABMovementBot : WoahBotBase
    {
        public MenuItem Enabledcheck;


        Waypoint currentwaypoint = new Waypoint();
        WaypointList StartToSt = new WaypointList("StartToSt");
        WaypointList TopGYToSt = new WaypointList("TopGYToSt");
        WaypointList Loop = new WaypointList("Loop");
        WaypointList GMToLoop = new WaypointList("GMToLoop");
        WaypointList LMtoLoop = new WaypointList("LMtoLoop");
        WaypointList FarmToLoop = new WaypointList("FarmToLoop");
        WaypointList BSToLoop = new WaypointList("BSToLoop");
        WaypointList waypoints = null;

        public ABMovementBot(WoahEnvironment env, WoahFish theform)
            : base(env, theform)
        {
            theform.HookKeyPress += new myKeyEventHandler(onKeyEvent);

            StartToSt.Add(new Vector3(1308.378f, 1312.165f, 0f));
            StartToSt.Add(new Vector3(1306.796f, 1310.339f, 0f));
            StartToSt.Add(new Vector3(1305.225f, 1308.526f, 0f));
            StartToSt.Add(new Vector3(1303.514f, 1306.733f, 0f));
            StartToSt.Add(new Vector3(1301.637f, 1304.807f, 0f));
            StartToSt.Add(new Vector3(1299.975f, 1303.089f, 0f));
            StartToSt.Add(new Vector3(1298.285f, 1301.342f, 0f));
            StartToSt.Add(new Vector3(1296.398f, 1299.392f, 0f));
            StartToSt.Add(new Vector3(1294.517f, 1297.448f, 0f));
            StartToSt.Add(new Vector3(1290.366f, 1293.158f, 0f), WaypointAction.Mount);
            StartToSt.Add(new Vector3(1287.306f, 1289.995f, 0f));
            StartToSt.Add(new Vector3(1284.105f, 1286.686f, 0f));
            StartToSt.Add(new Vector3(1281.231f, 1283.716f, 0f));
            StartToSt.Add(new Vector3(1277.318f, 1279.672f, 0f));
            StartToSt.Add(new Vector3(1274.059f, 1276.303f, 0f));
            StartToSt.Add(new Vector3(1270.367f, 1272.488f, 0f));
            StartToSt.Add(new Vector3(1266.98f, 1268.986f, 0f));
            StartToSt.Add(new Vector3(1263.031f, 1264.905f, 0f));
            StartToSt.Add(new Vector3(1259.597f, 1261.355f, 0f));
            StartToSt.Add(new Vector3(1255.508f, 1257.13f, 0f));
            StartToSt.Add(new Vector3(1251.98f, 1253.483f, 0f));
            StartToSt.Add(new Vector3(1248.604f, 1249.994f, 0f));
            StartToSt.Add(new Vector3(1245.24f, 1246.517f, 0f));
            StartToSt.Add(new Vector3(1241.163f, 1242.303f, 0f));
            StartToSt.Add(new Vector3(1237.799f, 1238.826f, 0f));
            StartToSt.Add(new Vector3(1233.804f, 1234.696f, 0f));
            StartToSt.Add(new Vector3(1229.984f, 1230.748f, 0f), WaypointAction.Stop);
            waypoints = StartToSt;

            TopGYToSt.Add(new Vector3(1276.273f, 1351.042f, 0f));
            TopGYToSt.Add(new Vector3(1279.93f, 1345.736f, 0f), WaypointAction.Mount);
            TopGYToSt.Add(new Vector3(1281.944f, 1340.215f, 0f));
            TopGYToSt.Add(new Vector3(1283.075f, 1334.585f, 0f));
            TopGYToSt.Add(new Vector3(1283.651f, 1328.819f, 0f));
            TopGYToSt.Add(new Vector3(1284.152f, 1323.027f, 0f));
            TopGYToSt.Add(new Vector3(1284.631f, 1317.504f, 0f));
            TopGYToSt.Add(new Vector3(1285.141f, 1311.613f, 0f));
            TopGYToSt.Add(new Vector3(1285.533f, 1306.553f, 0f));
            TopGYToSt.Add(new Vector3(1285.327f, 1301.123f, 0f));
            TopGYToSt.Add(new Vector3(1284.536f, 1295.993f, 0f));
            TopGYToSt.Add(new Vector3(1283.499f, 1291.241f, 0f));
            TopGYToSt.Add(new Vector3(1281.29f, 1285.556f, 0f));
            TopGYToSt.Add(new Vector3(1280.674f, 1283.783f, 0f));
            TopGYToSt.Add(new Vector3(1277.585f, 1280.719f, 0f));
            TopGYToSt.Add(new Vector3(1274.269f, 1277.429f, 0f));
            TopGYToSt.Add(new Vector3(1270.871f, 1274.057f, 0f));
            TopGYToSt.Add(new Vector3(1267.4f, 1270.613f, 0f));
            TopGYToSt.Add(new Vector3(1263.5f, 1266.744f, 0f));
            TopGYToSt.Add(new Vector3(1259.636f, 1262.91f, 0f));
            TopGYToSt.Add(new Vector3(1256.583f, 1259.881f, 0f));
            TopGYToSt.Add(new Vector3(1252.97f, 1256.295f, 0f));
            TopGYToSt.Add(new Vector3(1249.309f, 1252.663f, 0f));
            TopGYToSt.Add(new Vector3(1246.065f, 1249.444f, 0f));
            TopGYToSt.Add(new Vector3(1242.714f, 1246.119f, 0f));
            TopGYToSt.Add(new Vector3(1238.933f, 1242.368f, 0f));
            TopGYToSt.Add(new Vector3(1235.141f, 1238.605f, 0f));
            TopGYToSt.Add(new Vector3(1232.183f, 1235.671f, 0f));
            TopGYToSt.Add(new Vector3(1229.333f, 1232.843f, 0f));
            TopGYToSt.Add(new Vector3(1225.565f, 1229.104f, 0f));
            TopGYToSt.Add(new Vector3(1221.14f, 1224.714f, 0f), WaypointAction.Stop);

            GMToLoop.Add(new Vector3(785.8836f, 1210.09f, -83.03897f));
            GMToLoop.Add(new Vector3(785.8329f, 1207.506f, -83.29646f), WaypointAction.Mount);
            GMToLoop.Add(new Vector3(785.9286f, 1204.812f, -83.27123f));
            GMToLoop.Add(new Vector3(786.1472f, 1202.093f, -83.35461f));
            GMToLoop.Add(new Vector3(786.6442f, 1199.01f, -83.65955f));
            GMToLoop.Add(new Vector3(787.1256f, 1195.812f, -84.05765f));
            GMToLoop.Add(new Vector3(787.5173f, 1192.611f, -84.841f));
            GMToLoop.Add(new Vector3(788.0559f, 1188.208f, -86.2514f));
            GMToLoop.Add(new Vector3(788.57f, 1184.006f, -87.97379f));
            GMToLoop.Add(new Vector3(789.1616f, 1179.17f, -89.79119f));
            GMToLoop.Add(new Vector3(790.382f, 1174.718f, -91.48409f));
            GMToLoop.Add(new Vector3(792.8944f, 1170.971f, -93.18425f));
            GMToLoop.Add(new Vector3(796.2791f, 1167.657f, -95.01041f));
            GMToLoop.Add(new Vector3(799.9218f, 1165.1f, -96.51549f));
            GMToLoop.Add(new Vector3(804.3414f, 1163.663f, -97.82988f));
            GMToLoop.Add(new Vector3(809.4835f, 1163.176f, -99.01588f));
            GMToLoop.Add(new Vector3(814.2436f, 1163.622f, -99.99999f));
            GMToLoop.Add(new Vector3(819.1055f, 1164.949f, -101.0005f));
            GMToLoop.Add(new Vector3(823.8442f, 1166.216f, -101.922f));
            GMToLoop.Add(new Vector3(828.8305f, 1167.239f, -103.0186f));
            GMToLoop.Add(new Vector3(833.5557f, 1168.198f, -103.5774f));
            GMToLoop.Add(new Vector3(838.6453f, 1169.22f, -104.4161f));
            GMToLoop.Add(new Vector3(843.0564f, 1169.398f, -105.2803f));
            GMToLoop.Add(new Vector3(848.0322f, 1168.889f, -106.3153f));
            GMToLoop.Add(new Vector3(851.8412f, 1168.248f, -107.1048f));
            GMToLoop.Add(new Vector3(855.2113f, 1167.462f, -107.7829f));
            GMToLoop.Add(new Vector3(858.8281f, 1166.425f, -108.5285f));
            GMToLoop.Add(new Vector3(862.9598f, 1165.171f, -109.4561f));
            GMToLoop.Add(new Vector3(867.0272f, 1163.938f, -109.8904f));
            GMToLoop.Add(new Vector3(870.8694f, 1162.772f, -109.3741f));
            GMToLoop.Add(new Vector3(875.2267f, 1161.452f, -108.5972f));
            GMToLoop.Add(new Vector3(879.5485f, 1161.027f, -108.6162f));
            GMToLoop.Add(new Vector3(883.8603f, 1160.893f, -109.3993f));
            GMToLoop.Add(new Vector3(888.4371f, 1159.698f, -111.6812f));
            GMToLoop.Add(new Vector3(893.0197f, 1158.818f, -111.661f));
            GMToLoop.Add(new Vector3(896.5599f, 1159.415f, -111.081f));
            GMToLoop.Add(new Vector3(900.4277f, 1160.819f, -110.2674f));
            GMToLoop.Add(new Vector3(903.7825f, 1162.203f, -109.5184f));
            GMToLoop.Add(new Vector3(907.5295f, 1163.981f, -108.6475f));
            GMToLoop.Add(new Vector3(911.7454f, 1166.286f, -107.5223f));
            GMToLoop.Add(new Vector3(915.9612f, 1168.704f, -106.3007f));
            GMToLoop.Add(new Vector3(920.6246f, 1171.512f, -104.8403f));
            GMToLoop.Add(new Vector3(924.8992f, 1174.085f, -103.3689f));
            GMToLoop.Add(new Vector3(929.0588f, 1176.589f, -101.9184f));
            GMToLoop.Add(new Vector3(933.3335f, 1179.163f, -100.4988f));
            GMToLoop.Add(new Vector3(938.0399f, 1181.996f, -99.10926f));
            GMToLoop.Add(new Vector3(942.3722f, 1184.605f, -97.75666f));
            GMToLoop.Add(new Vector3(946.5893f, 1187.144f, -96.45341f));
            GMToLoop.Add(new Vector3(950.4322f, 1189.457f, -95.3166f));
            GMToLoop.Add(new Vector3(954.7357f, 1192.048f, -94.13523f));
            GMToLoop.Add(new Vector3(959.4854f, 1194.908f, -92.67809f));
            GMToLoop.Add(new Vector3(963.2631f, 1197.182f, -91.52506f));
            GMToLoop.Add(new Vector3(967.2787f, 1199.6f, -90.1978f));
            GMToLoop.Add(new Vector3(975.569f, 1204.591f, -87.47723f));
            GMToLoop.Add(new Vector3(979.5897f, 1206.861f, -86.2847f));
            GMToLoop.Add(new Vector3(983.7945f, 1208.652f, -85.22477f));
            GMToLoop.Add(new Vector3(988.1588f, 1210.001f, -84.04302f));
            GMToLoop.Add(new Vector3(994.5842f, 1211.725f, -82.60404f));
            GMToLoop.Add(new Vector3(998.6319f, 1212.781f, -81.65431f));
            GMToLoop.Add(new Vector3(1003.293f, 1213.998f, -80.44897f));
            GMToLoop.Add(new Vector3(1007.571f, 1215.103f, -79.35853f));
            GMToLoop.Add(new Vector3(1012.124f, 1215.975f, -78.16718f));
            GMToLoop.Add(new Vector3(1016.546f, 1216.619f, -77.15862f));
            GMToLoop.Add(new Vector3(1022.37f, 1216.892f, -75.89442f));
            GMToLoop.Add(new Vector3(1026.8f, 1216.702f, -74.78281f));
            GMToLoop.Add(new Vector3(1031.729f, 1216.196f, -73.55173f));
            GMToLoop.Add(new Vector3(1036.485f, 1215.516f, -72.35403f));
            GMToLoop.Add(new Vector3(1041.881f, 1214.48f, -70.91357f));
            GMToLoop.Add(new Vector3(1046.872f, 1213.248f, -69.86142f));
            GMToLoop.Add(new Vector3(1051.256f, 1212.086f, -69.17905f));
            GMToLoop.Add(new Vector3(1055.396f, 1210.985f, -68.44585f));
            GMToLoop.Add(new Vector3(1060.407f, 1209.338f, -67.52673f));
            GMToLoop.Add(new Vector3(1065.268f, 1207.716f, -66.41233f));
            GMToLoop.Add(new Vector3(1069.953f, 1206.153f, -65.20775f));
            GMToLoop.Add(new Vector3(1074.336f, 1204.691f, -64.70837f));
            GMToLoop.Add(new Vector3(1078.113f, 1203.431f, -64.57527f));
            GMToLoop.Add(new Vector3(1083.643f, 1201.586f, -64.13426f));
            GMToLoop.Add(new Vector3(1088.519f, 1199.959f, -63.54243f));
            GMToLoop.Add(new Vector3(1092.472f, 1198.64f, -63.03627f));
            GMToLoop.Add(new Vector3(1097.555f, 1196.944f, -62.61936f));
            GMToLoop.Add(new Vector3(1101.253f, 1195.711f, -62.24903f));
            GMToLoop.Add(new Vector3(1105.858f, 1194.174f, -61.94471f), WaypointAction.Stop);

            LMtoLoop.Add(new Vector3(1206.202f, 778.4471f, 15.79751f));
            LMtoLoop.Add(new Vector3(1206.154f, 780.3785f, 15.79751f), WaypointAction.Mount);
            LMtoLoop.Add(new Vector3(1206.303f, 783.613f, 15.58849f));
            LMtoLoop.Add(new Vector3(1207.081f, 787.3279f, 15.16178f));
            LMtoLoop.Add(new Vector3(1208.077f, 791.0085f, 14.63906f));
            LMtoLoop.Add(new Vector3(1208.868f, 793.7878f, 14.27628f));
            LMtoLoop.Add(new Vector3(1209.581f, 796.2923f, 13.93557f));
            LMtoLoop.Add(new Vector3(1210.353f, 799.007f, 13.43112f));
            LMtoLoop.Add(new Vector3(1211.057f, 801.4792f, 13.09278f));
            LMtoLoop.Add(new Vector3(1211.751f, 803.9191f, 12.55837f));
            LMtoLoop.Add(new Vector3(1212.56f, 806.763f, 12.11127f));
            LMtoLoop.Add(new Vector3(1213.14f, 808.799f, 11.87657f));
            LMtoLoop.Add(new Vector3(1213.81f, 811.633f, 11.88297f));
            LMtoLoop.Add(new Vector3(1214.062f, 814.2525f, 11.86284f));
            LMtoLoop.Add(new Vector3(1213.701f, 816.8632f, 11.85763f));
            LMtoLoop.Add(new Vector3(1212.93f, 819.415f, 11.85763f));
            LMtoLoop.Add(new Vector3(1211.84f, 821.4988f, 11.85763f));
            LMtoLoop.Add(new Vector3(1210.53f, 823.9415f, 11.85763f));
            LMtoLoop.Add(new Vector3(1209.172f, 826.473f, 11.89558f));
            LMtoLoop.Add(new Vector3(1208.028f, 828.6047f, 12.05941f));
            LMtoLoop.Add(new Vector3(1206.773f, 830.9437f, 11.95446f));
            LMtoLoop.Add(new Vector3(1205.581f, 833.1643f, 12.31543f));
            LMtoLoop.Add(new Vector3(1204.374f, 835.4145f, 12.68683f));
            LMtoLoop.Add(new Vector3(1203.583f, 837.2637f, 12.9971f));
            LMtoLoop.Add(new Vector3(1202.638f, 841.0104f, 13.01385f));
            LMtoLoop.Add(new Vector3(1202.063f, 843.995f, 13.02408f));
            LMtoLoop.Add(new Vector3(1201.722f, 846.9658f, 13.09945f));
            LMtoLoop.Add(new Vector3(1201.46f, 849.8097f, 13.02655f));
            LMtoLoop.Add(new Vector3(1201.186f, 852.8214f, 12.62126f));
            LMtoLoop.Add(new Vector3(1201.026f, 854.8815f, 12.58236f));
            LMtoLoop.Add(new Vector3(1200.804f, 858.3857f, 11.9901f));
            LMtoLoop.Add(new Vector3(1200.614f, 862.0095f, 11.11497f));
            LMtoLoop.Add(new Vector3(1200.524f, 864.1412f, 10.61766f));
            LMtoLoop.Add(new Vector3(1200.377f, 867.7502f, 9.887278f));
            LMtoLoop.Add(new Vector3(1200.24f, 871.0906f, 9.415838f));
            LMtoLoop.Add(new Vector3(1200.108f, 874.3135f, 8.936704f));
            LMtoLoop.Add(new Vector3(1199.988f, 877.2678f, 8.213638f));
            LMtoLoop.Add(new Vector3(1199.87f, 880.1382f, 7.183507f));
            LMtoLoop.Add(new Vector3(1199.75f, 883.0926f, 5.565878f));
            LMtoLoop.Add(new Vector3(1199.731f, 886.3337f, 3.774677f));
            LMtoLoop.Add(new Vector3(1199.868f, 889.3037f, 2.320737f));
            LMtoLoop.Add(new Vector3(1200.11f, 892.0313f, 0.9708925f));
            LMtoLoop.Add(new Vector3(1200.57f, 895.2748f, -0.6899896f));
            LMtoLoop.Add(new Vector3(1201.204f, 898.0381f, -2.172813f));
            LMtoLoop.Add(new Vector3(1202.322f, 900.9658f, -3.783964f));
            LMtoLoop.Add(new Vector3(1203.776f, 903.5375f, -5.371196f));
            LMtoLoop.Add(new Vector3(1205.414f, 905.956f, -6.679133f));
            LMtoLoop.Add(new Vector3(1207.426f, 908.3471f, -7.865434f));
            LMtoLoop.Add(new Vector3(1209.172f, 910.4777f, -8.74527f));
            LMtoLoop.Add(new Vector3(1210.67f, 912.7662f, -9.813243f));
            LMtoLoop.Add(new Vector3(1211.663f, 914.8229f, -10.737f));
            LMtoLoop.Add(new Vector3(1212.886f, 918.11f, -12.2121f));
            LMtoLoop.Add(new Vector3(1213.654f, 920.7911f, -13.38181f));
            LMtoLoop.Add(new Vector3(1214.503f, 923.623f, -14.58687f));
            LMtoLoop.Add(new Vector3(1215.431f, 925.9831f, -15.69374f));
            LMtoLoop.Add(new Vector3(1216.619f, 928.4124f, -16.97063f));
            LMtoLoop.Add(new Vector3(1218.038f, 930.8116f, -18.12407f));
            LMtoLoop.Add(new Vector3(1219.272f, 932.6339f, -18.9457f));
            LMtoLoop.Add(new Vector3(1220.61f, 934.609f, -20.11841f));
            LMtoLoop.Add(new Vector3(1221.969f, 936.8459f, -21.53316f));
            LMtoLoop.Add(new Vector3(1223.142f, 939.3574f, -23.32117f));
            LMtoLoop.Add(new Vector3(1224.104f, 942.0936f, -25.20882f));
            LMtoLoop.Add(new Vector3(1224.692f, 944.613f, -26.6951f));
            LMtoLoop.Add(new Vector3(1224.923f, 947.4343f, -28.3658f));
            LMtoLoop.Add(new Vector3(1224.729f, 950.0135f, -29.88731f));
            LMtoLoop.Add(new Vector3(1224.198f, 952.5751f, -31.4136f));
            LMtoLoop.Add(new Vector3(1223.397f, 955.4561f, -33.16027f));
            LMtoLoop.Add(new Vector3(1222.468f, 957.938f, -34.63146f));
            LMtoLoop.Add(new Vector3(1221.015f, 960.3947f, -36.00092f));
            LMtoLoop.Add(new Vector3(1219.154f, 962.9225f, -37.14167f));
            LMtoLoop.Add(new Vector3(1217.367f, 965.0416f, -38.3418f));
            LMtoLoop.Add(new Vector3(1215.45f, 967.3149f, -39.74756f));
            LMtoLoop.Add(new Vector3(1213.618f, 969.2743f, -40.52399f));
            LMtoLoop.Add(new Vector3(1211.732f, 970.7072f, -41.29117f));
            LMtoLoop.Add(new Vector3(1208.788f, 972.4246f, -42.87835f));
            LMtoLoop.Add(new Vector3(1206.281f, 973.6426f, -44.41303f));
            LMtoLoop.Add(new Vector3(1203.664f, 974.6408f, -45.03608f));
            LMtoLoop.Add(new Vector3(1200.692f, 975.2816f, -46.1981f));
            LMtoLoop.Add(new Vector3(1198.017f, 975.5093f, -46.98706f));
            LMtoLoop.Add(new Vector3(1195.018f, 975.3145f, -47.67471f));
            LMtoLoop.Add(new Vector3(1192.285f, 974.8581f, -47.82223f));
            LMtoLoop.Add(new Vector3(1189.249f, 974.1902f, -48.33599f));
            LMtoLoop.Add(new Vector3(1186.264f, 973.5303f, -49.28485f));
            LMtoLoop.Add(new Vector3(1183.475f, 972.9139f, -50.35431f));
            LMtoLoop.Add(new Vector3(1180.703f, 972.3011f, -51.29065f), WaypointAction.Stop);

            FarmToLoop.Add(new Vector3(795.1281f, 830.7887f, -57.22251f));
            FarmToLoop.Add(new Vector3(796.4594f, 829.1f, -57.16383f), WaypointAction.Mount);
            FarmToLoop.Add(new Vector3(796.4594f, 829.1f, -57.16383f));
            FarmToLoop.Add(new Vector3(797.758f, 827.7875f, -57.20914f));
            FarmToLoop.Add(new Vector3(798.5098f, 827.2228f, -57.23763f));
            FarmToLoop.Add(new Vector3(800.2179f, 826.8569f, -57.32158f));
            FarmToLoop.Add(new Vector3(800.5845f, 826.8101f, -57.39701f));
            FarmToLoop.Add(new Vector3(803.8235f, 827.9515f, -58.04427f));
            FarmToLoop.Add(new Vector3(806.741f, 830.0078f, -58.1801f));
            FarmToLoop.Add(new Vector3(808.3881f, 832.3624f, -58.16346f));
            FarmToLoop.Add(new Vector3(810.6066f, 836.0624f, -57.85703f));
            FarmToLoop.Add(new Vector3(813.1874f, 838.5891f, -57.41952f), WaypointAction.Jump);
            FarmToLoop.Add(new Vector3(815.6794f, 840.9662f, -57.38873f), WaypointAction.Jump);
            FarmToLoop.Add(new Vector3(817.9841f, 843.5757f, -57.91659f), WaypointAction.Jump);
            FarmToLoop.Add(new Vector3(819.4599f, 846.6976f, -58.26616f));
            FarmToLoop.Add(new Vector3(819.8121f, 850.3538f, -57.93953f));
            FarmToLoop.Add(new Vector3(819.884f, 854.0322f, -57.91753f));
            FarmToLoop.Add(new Vector3(819.3644f, 857.988f, -58.5304f));
            FarmToLoop.Add(new Vector3(818.0204f, 861.1383f, -58.49541f));
            FarmToLoop.Add(new Vector3(815.912f, 863.6667f, -58.18646f));
            FarmToLoop.Add(new Vector3(813.1061f, 865.0941f, -57.31512f));
            FarmToLoop.Add(new Vector3(809.7078f, 865.7253f, -55.72881f));
            FarmToLoop.Add(new Vector3(806.4274f, 864.9641f, -55.25853f));
            FarmToLoop.Add(new Vector3(803.4158f, 863.5127f, -55.50206f));
            FarmToLoop.Add(new Vector3(800.1616f, 862.145f, -55.26076f));
            FarmToLoop.Add(new Vector3(796.7363f, 862.2986f, -54.60517f));
            FarmToLoop.Add(new Vector3(793.0507f, 862.728f, -54.04467f));
            FarmToLoop.Add(new Vector3(789.551f, 862.4565f, -53.43413f));
            FarmToLoop.Add(new Vector3(786.0446f, 862.6757f, -52.85496f));
            FarmToLoop.Add(new Vector3(782.8718f, 864.3135f, -52.70085f));
            FarmToLoop.Add(new Vector3(780.5236f, 866.9381f, -51.78724f));
            FarmToLoop.Add(new Vector3(778.6577f, 870.1756f, -49.86159f));
            FarmToLoop.Add(new Vector3(777.6866f, 873.5088f, -48.27922f), WaypointAction.Stop);

            BSToLoop.Add(new Vector3(961.6844f, 1017.137f, -43.07496f));
            BSToLoop.Add(new Vector3(965.8973f, 1017.319f, -43.42021f), WaypointAction.Mount);
            BSToLoop.Add(new Vector3(970.5634f, 1017.521f, -43.67847f));
            BSToLoop.Add(new Vector3(975.2798f, 1017.725f, -43.47332f));
            BSToLoop.Add(new Vector3(980.6111f, 1018.452f, -43.32888f));
            BSToLoop.Add(new Vector3(985.1782f, 1019.477f, -43.27882f));
            BSToLoop.Add(new Vector3(989.9667f, 1021.435f, -43.52765f));
            BSToLoop.Add(new Vector3(994.1495f, 1023.874f, -44.2516f));
            BSToLoop.Add(new Vector3(997.7984f, 1027.293f, -45.33707f));
            BSToLoop.Add(new Vector3(1001.097f, 1031.917f, -46.46806f));
            BSToLoop.Add(new Vector3(1002.905f, 1036.375f, -46.22149f));
            BSToLoop.Add(new Vector3(1003.791f, 1041.555f, -46.20615f));
            BSToLoop.Add(new Vector3(1003.964f, 1046.518f, -47.5313f));
            BSToLoop.Add(new Vector3(1003.757f, 1051.789f, -48.95147f));
            BSToLoop.Add(new Vector3(1003.546f, 1057.161f, -50.55431f));
            BSToLoop.Add(new Vector3(1003.347f, 1062.214f, -51.88081f));
            BSToLoop.Add(new Vector3(1003.14f, 1067.502f, -53.03259f));
            BSToLoop.Add(new Vector3(1002.928f, 1072.874f, -55.15494f));
            BSToLoop.Add(new Vector3(1002.747f, 1077.49f, -56.56112f));
            BSToLoop.Add(new Vector3(1002.509f, 1083.046f, -58.11873f));
            BSToLoop.Add(new Vector3(1002.112f, 1088.356f, -59.25094f));
            BSToLoop.Add(new Vector3(1001.725f, 1093.533f, -60.34403f));
            BSToLoop.Add(new Vector3(1001.332f, 1098.794f, -61.01732f));
            BSToLoop.Add(new Vector3(1000.974f, 1103.585f, -61.39913f));
            BSToLoop.Add(new Vector3(1000.593f, 1108.678f, -61.76467f));
            BSToLoop.Add(new Vector3(1000.188f, 1114.089f, -61.12132f));
            BSToLoop.Add(new Vector3(999.819f, 1119.031f, -60.42759f));
            BSToLoop.Add(new Vector3(999.4358f, 1124.158f, -60.45341f));
            BSToLoop.Add(new Vector3(999.0225f, 1129.687f, -60.84882f));
            BSToLoop.Add(new Vector3(998.6631f, 1134.495f, -61.7763f));
            BSToLoop.Add(new Vector3(998.3162f, 1139.135f, -62.99267f));
            BSToLoop.Add(new Vector3(997.9442f, 1144.111f, -63.71024f));
            BSToLoop.Add(new Vector3(997.4871f, 1150.226f, -63.80075f), WaypointAction.Stop);

            Loop.Add(new Vector3(1216.459f, 1229.462f, 0f));
            Loop.Add(new Vector3(1218.676f, 1222.817f, 0f));
            Loop.Add(new Vector3(1220.817f, 1215.937f, 0f));
            Loop.Add(new Vector3(1222.6f, 1209.024f, 0f));
            Loop.Add(new Vector3(1223.654f, 1202.678f, 0f));
            Loop.Add(new Vector3(1224.365f, 1195.929f, 0f));
            Loop.Add(new Vector3(1224.989f, 1188.715f, 0f));
            Loop.Add(new Vector3(1225.384f, 1184.162f, 0f));
            Loop.Add(new Vector3(1226.329f, 1173.25f, 0f));
            Loop.Add(new Vector3(1226.843f, 1167.308f, 0f));
            Loop.Add(new Vector3(1227.107f, 1160.545f, 0f));
            Loop.Add(new Vector3(1227.188f, 1153.036f, 0f));
            Loop.Add(new Vector3(1227.188f, 1145.93f, 0f));
            Loop.Add(new Vector3(1227.188f, 1139.495f, 0f));
            Loop.Add(new Vector3(1227.188f, 1132.927f, 0f));
            Loop.Add(new Vector3(1227.188f, 1126.526f, 0f));
            Loop.Add(new Vector3(1226.712f, 1117.41f, 0f));
            Loop.Add(new Vector3(1226.047f, 1110.925f, 0f));
            Loop.Add(new Vector3(1225.381f, 1104.424f, 0f));
            Loop.Add(new Vector3(1224.633f, 1097.121f, 0f));
            Loop.Add(new Vector3(1223.739f, 1089.362f, 0f));
            Loop.Add(new Vector3(1222.616f, 1082.447f, 0f));
            Loop.Add(new Vector3(1221.448f, 1075.472f, 0f));
            Loop.Add(new Vector3(1219.986f, 1069.412f, 0f));
            Loop.Add(new Vector3(1217.675f, 1062.567f, 0f));
            Loop.Add(new Vector3(1215.459f, 1056.01f, 0f));
            Loop.Add(new Vector3(1212.931f, 1048.53f, 0f));
            Loop.Add(new Vector3(1210.575f, 1041.559f, 0f));
            Loop.Add(new Vector3(1208.209f, 1034.645f, 0f));
            Loop.Add(new Vector3(1205.687f, 1028.004f, 0f));
            Loop.Add(new Vector3(1202.949f, 1021.61f, 0f));
            Loop.Add(new Vector3(1200.166f, 1015.107f, 0f));
            Loop.Add(new Vector3(1196.933f, 1008.331f, 0f));
            Loop.Add(new Vector3(1193.379f, 1002.296f, 0f));
            Loop.Add(new Vector3(1189.202f, 995.5754f, 0f));
            Loop.Add(new Vector3(1185.15f, 989.3531f, 0f));
            Loop.Add(new Vector3(1181.538f, 983.8065f, 0f));
            Loop.Add(new Vector3(1178.2f, 978.6822f, 0f));
            Loop.Add(new Vector3(1174.652f, 973.2341f, 0f));
            Loop.Add(new Vector3(1171.012f, 967.6453f, 0f));
            Loop.Add(new Vector3(1166.987f, 962.0844f, 0f));
            Loop.Add(new Vector3(1159.998f, 952.5433f, 0f));
            Loop.Add(new Vector3(1156.119f, 947.4962f, 0f));
            Loop.Add(new Vector3(1151.483f, 942.3361f, 0f));
            Loop.Add(new Vector3(1145.547f, 936.0645f, 0f));
            Loop.Add(new Vector3(1140.489f, 930.7203f, 0f));
            Loop.Add(new Vector3(1135.465f, 925.4127f, 0f));
            Loop.Add(new Vector3(1130.812f, 920.4955f, 0f));
            Loop.Add(new Vector3(1126.885f, 916.347f, 0f));
            Loop.Add(new Vector3(1121.323f, 910.485f, 0f));
            Loop.Add(new Vector3(1112.671f, 901.5573f, 0f));
            Loop.Add(new Vector3(1107.979f, 896.9173f, 0f));
            Loop.Add(new Vector3(1102.629f, 892.1634f, 0f));
            Loop.Add(new Vector3(1097.297f, 887.4927f, 0f));
            Loop.Add(new Vector3(1092.034f, 883.6946f, 0f));
            Loop.Add(new Vector3(1085.729f, 879.9327f, 0f));
            Loop.Add(new Vector3(1079.771f, 876.3773f, 0f));
            Loop.Add(new Vector3(1073.856f, 872.8478f, 0f));
            Loop.Add(new Vector3(1067.491f, 869.092f, 0f));
            Loop.Add(new Vector3(1061.41f, 866.839f, 0f));
            Loop.Add(new Vector3(1053.592f, 863.9549f, 0f));
            Loop.Add(new Vector3(1047.508f, 861.7104f, 0f));
            Loop.Add(new Vector3(1039.974f, 858.9309f, 0f));
            Loop.Add(new Vector3(1033.323f, 856.4771f, 0f));
            Loop.Add(new Vector3(1027.018f, 854.1512f, 0f));
            Loop.Add(new Vector3(1021.029f, 851.9415f, 0f));
            Loop.Add(new Vector3(1015.318f, 850.0064f, 0f));
            Loop.Add(new Vector3(1008.326f, 847.7651f, 0f));
            Loop.Add(new Vector3(1001.592f, 845.6061f, 0f));
            Loop.Add(new Vector3(995.8485f, 843.7648f, 0f));
            Loop.Add(new Vector3(989.1209f, 841.6378f, 0f));
            Loop.Add(new Vector3(982.3527f, 839.5846f, 0f));
            Loop.Add(new Vector3(975.9381f, 837.6387f, 0f));
            Loop.Add(new Vector3(969.1699f, 835.5856f, 0f));
            Loop.Add(new Vector3(961.8107f, 833.8666f, 0f));
            Loop.Add(new Vector3(955.8376f, 832.5561f, 0f));
            Loop.Add(new Vector3(949.0276f, 831.0619f, 0f));
            Loop.Add(new Vector3(942.3325f, 829.593f, 0f));
            Loop.Add(new Vector3(934.4393f, 827.9599f, 0f));
            Loop.Add(new Vector3(927.5732f, 827.2291f, 0f));
            Loop.Add(new Vector3(921.0571f, 827.217f, 0f));
            Loop.Add(new Vector3(913.5455f, 827.6896f, 0f));
            Loop.Add(new Vector3(906.6647f, 828.4548f, 0f));
            Loop.Add(new Vector3(900.7788f, 829.7705f, 0f));
            Loop.Add(new Vector3(894.2455f, 831.3387f, 0f));
            Loop.Add(new Vector3(887.1155f, 833.2892f, 0f));
            Loop.Add(new Vector3(880.4392f, 835.1156f, 0f));
            Loop.Add(new Vector3(874.9423f, 836.9677f, 0f));
            Loop.Add(new Vector3(868.8471f, 839.6338f, 0f));
            Loop.Add(new Vector3(862.4595f, 842.4278f, 0f));
            Loop.Add(new Vector3(856.223f, 845.1893f, 0f));
            Loop.Add(new Vector3(850.7122f, 848.1249f, 0f));
            Loop.Add(new Vector3(844.605f, 851.7367f, 0f));
            Loop.Add(new Vector3(838.4022f, 855.3775f, 0f));
            Loop.Add(new Vector3(832.0966f, 858.3118f, 0f));
            Loop.Add(new Vector3(825.8086f, 861.2048f, 0f));
            Loop.Add(new Vector3(820.0212f, 863.1292f, 0f));
            Loop.Add(new Vector3(815.7628f, 863.7252f, 0f));
            Loop.Add(new Vector3(812.9822f, 863.9379f, 0f));
            Loop.Add(new Vector3(809.479f, 864.1664f, 0f));
            Loop.Add(new Vector3(805.8335f, 864.1903f, 0f));
            Loop.Add(new Vector3(802.3318f, 863.968f, 0f));
            Loop.Add(new Vector3(798.961f, 863.4503f, 0f));
            Loop.Add(new Vector3(796.005f, 862.7607f, 0f));
            Loop.Add(new Vector3(792.5613f, 861.4686f, 0f));
            Loop.Add(new Vector3(788.9574f, 860.9871f, 0f));
            Loop.Add(new Vector3(785.8937f, 860.9151f, 0f));
            Loop.Add(new Vector3(781.9262f, 861.9993f, 0f));
            Loop.Add(new Vector3(779.636f, 863.9338f, 0f));
            Loop.Add(new Vector3(777.3453f, 867.1946f, 0f));
            Loop.Add(new Vector3(776.3428f, 870.5461f, 0f));
            Loop.Add(new Vector3(775.8511f, 875.3148f, 0f));
            Loop.Add(new Vector3(776.0175f, 878.8051f, 0f));
            Loop.Add(new Vector3(776.1992f, 882.3621f, 0f));
            Loop.Add(new Vector3(776.4646f, 886.0642f, 0f));
            Loop.Add(new Vector3(776.9227f, 889.9348f, 0f));
            Loop.Add(new Vector3(777.408f, 893.7681f, 0f));
            Loop.Add(new Vector3(777.9117f, 897.6331f, 0f));
            Loop.Add(new Vector3(778.4414f, 901.6979f, 0f));
            Loop.Add(new Vector3(779.0015f, 905.996f, 0f));
            Loop.Add(new Vector3(779.6852f, 909.9273f, 0f));
            Loop.Add(new Vector3(781.1835f, 914.456f, 0f));
            Loop.Add(new Vector3(782.816f, 918.4672f, 0f));
            Loop.Add(new Vector3(784.7351f, 921.8781f, 0f));
            Loop.Add(new Vector3(786.9531f, 925.245f, 0f));
            Loop.Add(new Vector3(789.7755f, 928.8807f, 0f));
            Loop.Add(new Vector3(792.7148f, 932.5748f, 0f));
            Loop.Add(new Vector3(795.0264f, 935.4371f, 0f));
            Loop.Add(new Vector3(798.5417f, 938.8808f, 0f));
            Loop.Add(new Vector3(801.6662f, 941.8849f, 0f));
            Loop.Add(new Vector3(805.2629f, 945.3432f, 0f));
            Loop.Add(new Vector3(808.6902f, 948.6384f, 0f));
            Loop.Add(new Vector3(811.7783f, 951.6077f, 0f));
            Loop.Add(new Vector3(814.7453f, 954.4604f, 0f));
            Loop.Add(new Vector3(818.0151f, 957.6043f, 0f));
            Loop.Add(new Vector3(821.1516f, 960.6201f, 0f));
            Loop.Add(new Vector3(824.2761f, 963.6242f, 0f));
            Loop.Add(new Vector3(827.2794f, 966.5119f, 0f));
            Loop.Add(new Vector3(830.2464f, 969.3647f, 0f));
            Loop.Add(new Vector3(833.1892f, 972.1942f, 0f));
            Loop.Add(new Vector3(836.1804f, 975.0703f, 0f));
            Loop.Add(new Vector3(839.1475f, 977.923f, 0f));
            Loop.Add(new Vector3(842.3688f, 981.0203f, 0f));
            Loop.Add(new Vector3(845.2874f, 983.8265f, 0f));
            Loop.Add(new Vector3(848.3099f, 986.766f, 0f));
            Loop.Add(new Vector3(850.7953f, 990.1461f, 0f));
            Loop.Add(new Vector3(852.7527f, 993.6699f, 0f));
            Loop.Add(new Vector3(854.6335f, 997.4436f, 0f));
            Loop.Add(new Vector3(856.3274f, 1001.47f, 0f));
            Loop.Add(new Vector3(857.872f, 1005.158f, 0f));
            Loop.Add(new Vector3(859.2726f, 1009.081f, 0f));
            Loop.Add(new Vector3(860.5606f, 1012.795f, 0f));
            Loop.Add(new Vector3(862.3219f, 1017.875f, 0f));
            Loop.Add(new Vector3(863.8466f, 1022.271f, 0f));
            Loop.Add(new Vector3(865.107f, 1025.906f, 0f));
            Loop.Add(new Vector3(866.7439f, 1030.566f, 0f));
            Loop.Add(new Vector3(868.1927f, 1034.383f, 0f));
            Loop.Add(new Vector3(869.9461f, 1038.253f, 0f));
            Loop.Add(new Vector3(871.8563f, 1041.861f, 0f));
            Loop.Add(new Vector3(873.8024f, 1045.295f, 0f));
            Loop.Add(new Vector3(876.041f, 1048.959f, 0f));
            Loop.Add(new Vector3(878.1897f, 1052.311f, 0f));
            Loop.Add(new Vector3(880.4776f, 1055.853f, 0f));
            Loop.Add(new Vector3(883.1587f, 1059.675f, 0f));
            Loop.Add(new Vector3(885.8602f, 1063.129f, 0f));
            Loop.Add(new Vector3(888.5883f, 1066.562f, 0f));
            Loop.Add(new Vector3(891.2594f, 1069.911f, 0f));
            Loop.Add(new Vector3(896.5734f, 1076.021f, 0f));
            Loop.Add(new Vector3(899.5154f, 1079.385f, 0f));
            Loop.Add(new Vector3(901.9377f, 1082.154f, 0f));
            Loop.Add(new Vector3(904.8466f, 1085.48f, 0f));
            Loop.Add(new Vector3(907.2687f, 1088.249f, 0f));
            Loop.Add(new Vector3(910.0339f, 1091.41f, 0f));
            Loop.Add(new Vector3(912.6773f, 1094.433f, 0f));
            Loop.Add(new Vector3(915.9069f, 1098.125f, 0f));
            Loop.Add(new Vector3(918.9043f, 1101.552f, 0f));
            Loop.Add(new Vector3(922.0122f, 1105.105f, 0f));
            Loop.Add(new Vector3(924.6114f, 1108.077f, 0f));
            Loop.Add(new Vector3(927.3876f, 1111.251f, 0f));
            Loop.Add(new Vector3(930.0531f, 1114.299f, 0f));
            Loop.Add(new Vector3(933.0173f, 1117.688f, 0f));
            Loop.Add(new Vector3(935.9265f, 1120.689f, 0f));
            Loop.Add(new Vector3(939.7703f, 1123.923f, 0f));
            Loop.Add(new Vector3(943.4226f, 1126.993f, 0f));
            Loop.Add(new Vector3(946.9205f, 1129.934f, 0f));
            Loop.Add(new Vector3(949.9426f, 1132.474f, 0f));
            Loop.Add(new Vector3(954.0964f, 1135.966f, 0f));
            Loop.Add(new Vector3(957.2214f, 1138.593f, 0f));
            Loop.Add(new Vector3(960.7822f, 1141.509f, 0f));
            Loop.Add(new Vector3(964.36f, 1144.158f, 0f));
            Loop.Add(new Vector3(968.7321f, 1147.016f, 0f));
            Loop.Add(new Vector3(972.8569f, 1149.447f, 0f));
            Loop.Add(new Vector3(977.2673f, 1151.741f, 0f));
            Loop.Add(new Vector3(981.2261f, 1153.587f, 0f));
            Loop.Add(new Vector3(985.2151f, 1155.282f, 0f));
            Loop.Add(new Vector3(989.4432f, 1156.875f, 0f));
            Loop.Add(new Vector3(994.1553f, 1158.516f, 0f));
            Loop.Add(new Vector3(998.6461f, 1159.858f, 0f));
            Loop.Add(new Vector3(1003.048f, 1161.147f, 0f));
            Loop.Add(new Vector3(1007.016f, 1162.3f, 0f));
            Loop.Add(new Vector3(1012.098f, 1163.776f, 0f));
            Loop.Add(new Vector3(1016.744f, 1165.126f, 0f));
            Loop.Add(new Vector3(1021.165f, 1166.411f, 0f));
            Loop.Add(new Vector3(1025.521f, 1167.676f, 0f));
            Loop.Add(new Vector3(1029.741f, 1168.903f, 0f));
            Loop.Add(new Vector3(1034.242f, 1170.21f, 0f));
            Loop.Add(new Vector3(1038.26f, 1171.377f, 0f));
            Loop.Add(new Vector3(1043.261f, 1172.83f, 0f));
            Loop.Add(new Vector3(1047.278f, 1173.998f, 0f));
            Loop.Add(new Vector3(1051.634f, 1175.263f, 0f));
            Loop.Add(new Vector3(1056.49f, 1176.674f, 0f));
            Loop.Add(new Vector3(1061.878f, 1178.239f, 0f));
            Loop.Add(new Vector3(1066.436f, 1179.564f, 0f));
            Loop.Add(new Vector3(1071.26f, 1180.967f, 0f));
            Loop.Add(new Vector3(1075.945f, 1182.489f, 0f));
            Loop.Add(new Vector3(1080.455f, 1184.069f, 0f));
            Loop.Add(new Vector3(1084.797f, 1185.696f, 0f));
            Loop.Add(new Vector3(1089.37f, 1187.424f, 0f));
            Loop.Add(new Vector3(1093.299f, 1188.909f, 0f));
            Loop.Add(new Vector3(1098.312f, 1190.803f, 0f));
            Loop.Add(new Vector3(1102.571f, 1192.412f, 0f));
            Loop.Add(new Vector3(1107.804f, 1194.39f, 0f));
            Loop.Add(new Vector3(1111.434f, 1195.762f, 0f));
            Loop.Add(new Vector3(1116.086f, 1197.519f, 0f));
            Loop.Add(new Vector3(1120.722f, 1199.271f, 0f));
            Loop.Add(new Vector3(1124.997f, 1200.886f, 0f));
            Loop.Add(new Vector3(1129.208f, 1202.478f, 0f));
            Loop.Add(new Vector3(1133.76f, 1204.33f, 0f));
            Loop.Add(new Vector3(1137.736f, 1206.617f, 0f));
            Loop.Add(new Vector3(1142.54f, 1209.382f, 0f));
            Loop.Add(new Vector3(1147.172f, 1212.048f, 0f));
            Loop.Add(new Vector3(1151.161f, 1214.345f, 0f));
            Loop.Add(new Vector3(1155.147f, 1216.613f, 0f));
            Loop.Add(new Vector3(1159.078f, 1218.739f, 0f));
            Loop.Add(new Vector3(1163.382f, 1220.912f, 0f));
            Loop.Add(new Vector3(1168.228f, 1223.199f, 0f));
            Loop.Add(new Vector3(1172.83f, 1225.112f, 0f));
            Loop.Add(new Vector3(1177.395f, 1226.716f, 0f));
            Loop.Add(new Vector3(1186.684f, 1229.978f, 0f));
            Loop.Add(new Vector3(1191.328f, 1231.609f, 0f));
            Loop.Add(new Vector3(1195.733f, 1233.102f, 0f));
            Loop.Add(new Vector3(1200.692f, 1234.08f, 0f));
            Loop.Add(new Vector3(1205.069f, 1234.301f, 0f));
            Loop.Add(new Vector3(1209.952f, 1234.057f, 0f));
            Loop.Add(new Vector3(1214.201f, 1233.657f, 0f));
            Loop.Add(new Vector3(1218.757f, 1232.725f, 0f));

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
                        waypoints = StartToSt;
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

                        if(needQ)
                        {
                            theform.joinAB();
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
                    else if(env.Location == "battleground")
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

                                waypoints = StartToSt;
                                WoahObject plyar = (WoahObject)env.Objects[env.PlayerID];
                                if (plyar != null)
                                {

                                    currentwaypoint = waypoints.FindClosestTo(new Vector3(plyar.X, plyar.Y, 0));
                                    targetpos = currentwaypoint.location;
                                }

                            }
                        }
                    }
                   
                    WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                    if (plyr != null)
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
                            if (Vector3.Distance(mypos, TopGYToSt.Get(0).location) < 100)
                            {
                                waypoints = TopGYToSt;
                            }
                            else if (Vector3.Distance(mypos, GMToLoop.Get(0).location) < 100)
                            {
                                waypoints = GMToLoop;
                            }
                            else if (Vector3.Distance(mypos, LMtoLoop.Get(0).location) < 100)
                            {
                                waypoints = LMtoLoop;
                            }
                            else if (Vector3.Distance(mypos, FarmToLoop.Get(0).location) < 100)
                            {
                                waypoints = FarmToLoop;
                            }
                            else if (Vector3.Distance(mypos, BSToLoop.Get(0).location) < 100)
                            {
                                waypoints = BSToLoop;
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
                        if (count % 10 == 0)
                        {
                            theform.AddMovementText(waypoints.Name + ": " + currentwaypoint.ID.ToString() + ": " + targetpos.ToString() + "  my pos: " + mypos.ToString());
                        }

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
                                if (waypoints.Name == StartToSt.Name)
                                {
                                    waypoints = Loop;
                                    currentwaypoint = waypoints.Get(0);
                                    targetpos = currentwaypoint.location;

                                }
                                else if (waypoints.Name == TopGYToSt.Name)
                                {
                                    waypoints = Loop;
                                    currentwaypoint = waypoints.Get(0);
                                    targetpos = currentwaypoint.location;
                                }
                                else if (waypoints.Name == GMToLoop.Name)
                                {
                                    waypoints = Loop;
                                    mypos = new Vector3(plyr.X, plyr.Y, 0);
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;

                                }
                                else if (waypoints.Name == LMtoLoop.Name)
                                {
                                    waypoints = Loop;
                                    mypos = new Vector3(plyr.X, plyr.Y, 0);
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;

                                }
                                else if (waypoints.Name == FarmToLoop.Name)
                                {
                                    waypoints = Loop;
                                    mypos = new Vector3(plyr.X, plyr.Y, 0);
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;

                                }
                                else if (waypoints.Name == BSToLoop.Name)
                                {
                                    waypoints = Loop;
                                    mypos = new Vector3(plyr.X, plyr.Y, 0);
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;

                                }
                                else
                                {
                                    theform.moveunclick();
                                }

                                Thread.Sleep(33);
                                continue;
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
                        if (failedcount >3)
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
