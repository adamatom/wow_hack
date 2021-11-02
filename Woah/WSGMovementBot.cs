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
    class WSGMovementBot : WoahBotBase
    {
        public MenuItem Enabledcheck;


        Waypoint currentwaypoint = new Waypoint();
        WaypointList startrun = new WaypointList("startrun");
        WaypointList deathrun1 = new WaypointList("deathrun1");
        WaypointList hordecircle = new WaypointList("hordecircle");
        WaypointList basenoafk = new WaypointList("basenoafk");
        WaypointList waypoints = null;

        public WSGMovementBot(WoahEnvironment env, WoahFish theform)
            : base(env, theform)
        {
            theform.HookKeyPress += new myKeyEventHandler(onKeyEvent);

            /*waypoints.Add(new Vector3(551.6987f, -8973.527f, 0f));
            waypoints.Add(new Vector3(546.7971f, -8970.284f, 0f));
            waypoints.Add(new Vector3(534.9166f, -8962.865f, 0f));
            waypoints.Add(new Vector3(519.846f, -8953.171f, 0f));
            waypoints.Add(new Vector3(508.8613f, -8944.913f, 0f));
            waypoints.Add(new Vector3(499.4898f, -8937.811f, 0f));
            waypoints.Add(new Vector3(493.4331f, -8926.456f, 0f));
            waypoints.Add(new Vector3(501.21f, -8917.415f, 0f));
            waypoints.Add(new Vector3(508.1008f, -8914.39f, 0f));
            waypoints.Add(new Vector3(521.85f, -8914.229f, 0f));
            waypoints.Add(new Vector3(532.2799f, -8918.844f, 0f));
            waypoints.Add(new Vector3(542.6995f, -8928.583f, 0f));
            waypoints.Add(new Vector3(551.5897f, -8936.307f, 0f));
            waypoints.Add(new Vector3(561.8097f, -8946.228f, 0f));
            waypoints.Add(new Vector3(562.7352f, -8960.868f, 0f));*/

            startrun.Add(new Vector3(1481.854f, 1522.475f, 0f));
            startrun.Add(new Vector3(1485.111f, 1517.434f, 0f));
            startrun.Add(new Vector3(1488.282f, 1514.347f, 0f));
            startrun.Add(new Vector3(1492.485f, 1510.003f, 0f));
            startrun.Add(new Vector3(1494.064f, 1505.783f, 0f));
            startrun.Add(new Vector3(1494.355f, 1502.302f, 0f));
            startrun.Add(new Vector3(1494.424f, 1497.876f, 0f));
            startrun.Add(new Vector3(1494.397f, 1493.304f, 0f));
            startrun.Add(new Vector3(1494.448f, 1488.74f, 0f));
            startrun.Add(new Vector3(1494.5f, 1484.305f, 0f));
            startrun.Add(new Vector3(1494.554f, 1479.749f, 0f));
            startrun.Add(new Vector3(1494.608f, 1475.185f, 0f));
            startrun.Add(new Vector3(1494.661f, 1470.621f, 0f));
            startrun.Add(new Vector3(1494.714f, 1466.178f, 0f));
            startrun.Add(new Vector3(1494.75f, 1463.071f, 0f));
            startrun.Add(new Vector3(1496.013f, 1456.721f, 0f));
            startrun.Add(new Vector3(1499.755f, 1451.529f, 0f));
            startrun.Add(new Vector3(1507.443f, 1445.935f, 0f), WaypointAction.Mount);
            startrun.Add(new Vector3(1515.211f, 1440.931f, 0f));
            startrun.Add(new Vector3(1520.624f, 1437.453f, 0f));
            startrun.Add(new Vector3(1527.385f, 1431.217f, 0f));
            startrun.Add(new Vector3(1530.172f, 1425.441f, 0f));
            startrun.Add(new Vector3(1532.461f, 1416.476f, 0f));
            startrun.Add(new Vector3(1534.466f, 1407.164f, 0f));
            startrun.Add(new Vector3(1536.474f, 1397.835f, 0f));
            startrun.Add(new Vector3(1535.059f, 1388.564f, 0f));
            startrun.Add(new Vector3(1533.227f, 1380.66f, 0f));
            startrun.Add(new Vector3(1531.774f, 1374.391f, 0f));
            startrun.Add(new Vector3(1529.623f, 1365.112f, 0f));
            startrun.Add(new Vector3(1527.545f, 1356.091f, 0f));
            startrun.Add(new Vector3(1526.136f, 1349.796f, 0f));
            startrun.Add(new Vector3(1524.274f, 1340.749f, 0f));
            startrun.Add(new Vector3(1522.821f, 1331.335f, 0f));
            startrun.Add(new Vector3(1521.368f, 1321.921f, 0f));
            startrun.Add(new Vector3(1519.915f, 1312.507f, 0f));
            startrun.Add(new Vector3(1518.77f, 1306.459f, 0f));
            startrun.Add(new Vector3(1514.089f, 1298.192f, 0f));
            startrun.Add(new Vector3(1509.103f, 1290.056f, 0f));
            startrun.Add(new Vector3(1505.89f, 1284.813f, 0f));
            startrun.Add(new Vector3(1500.892f, 1276.665f, 0f));
            startrun.Add(new Vector3(1495.432f, 1268.882f, 0f));
            startrun.Add(new Vector3(1490.08f, 1261.351f, 0f));
            startrun.Add(new Vector3(1487.158f, 1255.6f, 0f));
            startrun.Add(new Vector3(1484.374f, 1246.796f, 0f));
            startrun.Add(new Vector3(1482.312f, 1237.46f, 0f));
            startrun.Add(new Vector3(1480.296f, 1228.219f, 0f));
            startrun.Add(new Vector3(1478.178f, 1218.915f, 0f));
            startrun.Add(new Vector3(1476.119f, 1209.872f, 0f));
            startrun.Add(new Vector3(1474.012f, 1200.617f, 0f));
            startrun.Add(new Vector3(1460.759f, 1142.399f, 0f));
            startrun.Add(new Vector3(1460.275f, 1140.218f, 0f));
            startrun.Add(new Vector3(1459.465f, 1130.75f, 0f));
            startrun.Add(new Vector3(1460.582f, 1121.02f, 0f));
            startrun.Add(new Vector3(1460.582f, 1121.02f, 0f), WaypointAction.Stop);
            waypoints = startrun;

            hordecircle.Add(new Vector3(1461.727f, 1111.657f, 0f));
            hordecircle.Add(new Vector3(1461.573f, 1108.642f, 0f));
            hordecircle.Add(new Vector3(1461.463f, 1105.585f, 0f));
            hordecircle.Add(new Vector3(1461.287f, 1100.734f, 0f));
            hordecircle.Add(new Vector3(1461.111f, 1095.859f, 0f));
            hordecircle.Add(new Vector3(1461.001f, 1092.834f, 0f));
            hordecircle.Add(new Vector3(1460.824f, 1089.397f, 0f));
            hordecircle.Add(new Vector3(1460.576f, 1084.928f, 0f));
            hordecircle.Add(new Vector3(1460.697f, 1079.935f, 0f));
            hordecircle.Add(new Vector3(1460.811f, 1077.007f, 0f));
            hordecircle.Add(new Vector3(1460.647f, 1071.955f, 0f));
            hordecircle.Add(new Vector3(1460.44f, 1069.041f, 0f));
            hordecircle.Add(new Vector3(1460.314f, 1063.886f, 0f));
            hordecircle.Add(new Vector3(1460.226f, 1059.613f, 0f));
            hordecircle.Add(new Vector3(1460.165f, 1056.643f, 0f));
            hordecircle.Add(new Vector3(1460.058f, 1051.379f, 0f));
            hordecircle.Add(new Vector3(1460.013f, 1048.305f, 0f));
            hordecircle.Add(new Vector3(1460.401f, 1043.491f, 0f));
            hordecircle.Add(new Vector3(1460.334f, 1038.896f, 0f));
            hordecircle.Add(new Vector3(1460.207f, 1035.736f, 0f));
            hordecircle.Add(new Vector3(1460.016f, 1030.967f, 0f));
            hordecircle.Add(new Vector3(1459.885f, 1027.709f, 0f));
            hordecircle.Add(new Vector3(1459.714f, 1023.424f, 0f));
            hordecircle.Add(new Vector3(1459.518f, 1018.542f, 0f));
            hordecircle.Add(new Vector3(1459.396f, 1015.51f, 0f));
            hordecircle.Add(new Vector3(1459.216f, 1011.013f, 0f));
            hordecircle.Add(new Vector3(1459.077f, 1007.555f, 0f));
            hordecircle.Add(new Vector3(1458.882f, 1002.68f, 0f));
            hordecircle.Add(new Vector3(1458.781f, 998.2003f, 0f));
            hordecircle.Add(new Vector3(1459.145f, 994.7825f, 0f));
            hordecircle.Add(new Vector3(1459.578f, 991.5037f, 0f));
            hordecircle.Add(new Vector3(1460.913f, 987.5497f, 0f));
            hordecircle.Add(new Vector3(1464.443f, 984.2236f, 0f));
            hordecircle.Add(new Vector3(1468.711f, 982.7615f, 0f));
            hordecircle.Add(new Vector3(1473.193f, 982.6346f, 0f));
            hordecircle.Add(new Vector3(1476.611f, 982.5011f, 0f));
            hordecircle.Add(new Vector3(1481.024f, 981.6884f, 0f));
            hordecircle.Add(new Vector3(1483.928f, 980.731f, 0f));
            hordecircle.Add(new Vector3(1488.444f, 978.8557f, 0f));
            hordecircle.Add(new Vector3(1492.539f, 976.952f, 0f));
            hordecircle.Add(new Vector3(1496.535f, 974.8322f, 0f));
            hordecircle.Add(new Vector3(1499.565f, 973.225f, 0f));
            hordecircle.Add(new Vector3(1502.246f, 971.8027f, 0f));
            hordecircle.Add(new Vector3(1506.185f, 969.6464f, 0f));
            hordecircle.Add(new Vector3(1510.075f, 966.6202f, 0f));
            hordecircle.Add(new Vector3(1512.512f, 964.3237f, 0f));
            hordecircle.Add(new Vector3(1515.488f, 960.9069f, 0f));
            hordecircle.Add(new Vector3(1518.071f, 957.2659f, 0f));
            hordecircle.Add(new Vector3(1514.381f, 962.6471f, 0f));
            hordecircle.Add(new Vector3(1516.775f, 962.7789f, 0f));
            hordecircle.Add(new Vector3(1519.745f, 962.0625f, 0f));
            hordecircle.Add(new Vector3(1523.549f, 959.7777f, 0f));
            hordecircle.Add(new Vector3(1525.413f, 956.929f, 0f));
            hordecircle.Add(new Vector3(1526.395f, 952.5101f, 0f));
            hordecircle.Add(new Vector3(1525.999f, 948.0353f, 0f));
            hordecircle.Add(new Vector3(1524.641f, 944.9686f, 0f));
            hordecircle.Add(new Vector3(1522.748f, 942.1599f, 0f));
            hordecircle.Add(new Vector3(1519.289f, 939.1893f, 0f));
            hordecircle.Add(new Vector3(1516.231f, 937.5291f, 0f));
            hordecircle.Add(new Vector3(1512.043f, 936.3553f, 0f));
            hordecircle.Add(new Vector3(1507.52f, 935.6678f, 0f));
            hordecircle.Add(new Vector3(1502.631f, 935.449f, 0f));
            hordecircle.Add(new Vector3(1499.188f, 935.3809f, 0f));
            hordecircle.Add(new Vector3(1494.744f, 935.6411f, 0f));
            hordecircle.Add(new Vector3(1491.291f, 935.8748f, 0f));
            hordecircle.Add(new Vector3(1486.769f, 936.1807f, 0f));
            hordecircle.Add(new Vector3(1482.048f, 936.5341f, 0f));
            hordecircle.Add(new Vector3(1478.878f, 936.7736f, 0f));
            hordecircle.Add(new Vector3(1472.914f, 937.2242f, 0f));
            hordecircle.Add(new Vector3(1469.751f, 937.4631f, 0f));
            hordecircle.Add(new Vector3(1465.315f, 937.1072f, 0f));
            hordecircle.Add(new Vector3(1462.871f, 935.3254f, 0f));
            hordecircle.Add(new Vector3(1461.501f, 931.0413f, 0f));
            hordecircle.Add(new Vector3(1462.273f, 923.1336f, 0f));
            hordecircle.Add(new Vector3(1458.992f, 923.6703f, 0f));
            hordecircle.Add(new Vector3(1458.263f, 926.8952f, 0f));
            hordecircle.Add(new Vector3(1458.482f, 931.4785f, 0f));
            hordecircle.Add(new Vector3(1458.644f, 934.8556f, 0f));
            hordecircle.Add(new Vector3(1458.937f, 940.9585f, 0f));
            hordecircle.Add(new Vector3(1459.1f, 944.3597f, 0f));
            hordecircle.Add(new Vector3(1459.21f, 949.2199f, 0f));
            hordecircle.Add(new Vector3(1459.237f, 952.3754f, 0f));
            hordecircle.Add(new Vector3(1459.279f, 957.2052f, 0f));
            hordecircle.Add(new Vector3(1459.317f, 961.6245f, 0f));
            hordecircle.Add(new Vector3(1459.356f, 966.1806f, 0f));
            hordecircle.Add(new Vector3(1459.386f, 969.6259f, 0f));
            hordecircle.Add(new Vector3(1459.36f, 974.1003f, 0f));
            hordecircle.Add(new Vector3(1459.116f, 978.6033f, 0f));
            hordecircle.Add(new Vector3(1458.875f, 983.3464f, 0f));
            hordecircle.Add(new Vector3(1458.899f, 986.3248f, 0f));
            hordecircle.Add(new Vector3(1458.939f, 990.9534f, 0f));
            hordecircle.Add(new Vector3(1458.978f, 995.4854f, 0f));
            hordecircle.Add(new Vector3(1459.005f, 998.5604f, 0f));
            hordecircle.Add(new Vector3(1459.046f, 1003.286f, 0f));
            hordecircle.Add(new Vector3(1459.085f, 1007.801f, 0f));
            hordecircle.Add(new Vector3(1459.128f, 1012.792f, 0f));
            hordecircle.Add(new Vector3(1459.205f, 1015.609f, 0f));
            hordecircle.Add(new Vector3(1459.356f, 1020.936f, 0f));
            hordecircle.Add(new Vector3(1459.47f, 1024.975f, 0f));
            hordecircle.Add(new Vector3(1459.998f, 1029.893f, 0f));
            hordecircle.Add(new Vector3(1461.567f, 1034.106f, 0f));
            hordecircle.Add(new Vector3(1463.115f, 1036.749f, 0f));
            hordecircle.Add(new Vector3(1466.635f, 1040.23f, 0f));
            hordecircle.Add(new Vector3(1470.1f, 1043.05f, 0f));
            hordecircle.Add(new Vector3(1473.642f, 1045.876f, 0f));
            hordecircle.Add(new Vector3(1477.506f, 1048.475f, 0f));
            hordecircle.Add(new Vector3(1480.2f, 1049.777f, 0f));
            hordecircle.Add(new Vector3(1485.072f, 1051.133f, 0f));
            hordecircle.Add(new Vector3(1490.078f, 1051.589f, 0f));
            hordecircle.Add(new Vector3(1494.481f, 1051.238f, 0f));
            hordecircle.Add(new Vector3(1498.901f, 1050.444f, 0f));
            hordecircle.Add(new Vector3(1502.241f, 1049.667f, 0f));
            hordecircle.Add(new Vector3(1506.894f, 1048.345f, 0f));
            hordecircle.Add(new Vector3(1509.8f, 1047.338f, 0f));
            hordecircle.Add(new Vector3(1514.407f, 1045.736f, 0f));
            hordecircle.Add(new Vector3(1517.317f, 1044.942f, 0f));
            hordecircle.Add(new Vector3(1521.797f, 1044.133f, 0f));
            hordecircle.Add(new Vector3(1526.715f, 1044.235f, 0f));
            hordecircle.Add(new Vector3(1529.568f, 1044.966f, 0f));
            hordecircle.Add(new Vector3(1533.749f, 1046.823f, 0f));
            hordecircle.Add(new Vector3(1536.446f, 1048.819f, 0f));
            hordecircle.Add(new Vector3(1539.599f, 1052.07f, 0f));
            hordecircle.Add(new Vector3(1542.447f, 1055.609f, 0f));
            hordecircle.Add(new Vector3(1544.088f, 1058.583f, 0f));
            hordecircle.Add(new Vector3(1545.697f, 1063.143f, 0f));
            hordecircle.Add(new Vector3(1546.63f, 1067.591f, 0f));
            hordecircle.Add(new Vector3(1546.416f, 1072.483f, 0f));
            hordecircle.Add(new Vector3(1545.926f, 1075.411f, 0f));
            hordecircle.Add(new Vector3(1544.158f, 1079.565f, 0f));
            hordecircle.Add(new Vector3(1541.753f, 1083.401f, 0f));
            hordecircle.Add(new Vector3(1538.482f, 1087.361f, 0f));
            hordecircle.Add(new Vector3(1536.62f, 1089.4f, 0f));
            hordecircle.Add(new Vector3(1532.717f, 1092.97f, 0f));
            hordecircle.Add(new Vector3(1528.999f, 1094.878f, 0f));
            hordecircle.Add(new Vector3(1526.038f, 1095.314f, 0f));
            hordecircle.Add(new Vector3(1522.84f, 1096.625f, 0f));
            hordecircle.Add(new Vector3(1520.424f, 1100.566f, 0f));
            hordecircle.Add(new Vector3(1520.341f, 1103.61f, 0f));
            hordecircle.Add(new Vector3(1519.308f, 1108.273f, 0f));
            hordecircle.Add(new Vector3(1517f, 1110.046f, 0f));
            hordecircle.Add(new Vector3(1512.323f, 1110.19f, 0f));
            hordecircle.Add(new Vector3(1509.416f, 1109.472f, 0f));
            hordecircle.Add(new Vector3(1504.896f, 1109.671f, 0f));
            hordecircle.Add(new Vector3(1500.822f, 1112.291f, 0f));
            hordecircle.Add(new Vector3(1498.269f, 1116.535f, 0f));
            hordecircle.Add(new Vector3(1496.804f, 1119.089f, 0f));
            hordecircle.Add(new Vector3(1493.173f, 1122.328f, 0f));
            hordecircle.Add(new Vector3(1490.523f, 1123.873f, 0f));
            hordecircle.Add(new Vector3(1485.922f, 1125.373f, 0f));
            hordecircle.Add(new Vector3(1481.504f, 1126.455f, 0f));
            hordecircle.Add(new Vector3(1478.126f, 1126.583f, 0f));
            hordecircle.Add(new Vector3(1473.291f, 1125.71f, 0f));
            hordecircle.Add(new Vector3(1469.137f, 1124.003f, 0f));
            hordecircle.Add(new Vector3(1466.185f, 1122.33f, 0f));
            hordecircle.Add(new Vector3(1463.178f, 1118.935f, 0f));
            hordecircle.Add(new Vector3(1461.901f, 1115.994f, 0f));

            deathrun1.Add(new Vector3(1554.781f, 1416.996f, 0f));
            deathrun1.Add(new Vector3(1554.471f, 1419.058f, 0f), WaypointAction.Jump);
            deathrun1.Add(new Vector3(1553.509f, 1425.458f, 0f), WaypointAction.Jump);
            deathrun1.Add(new Vector3(1552.558f, 1431.779f, 0f), WaypointAction.Jump);
            deathrun1.Add(new Vector3(1550.141f, 1437.15f, 0f));
            deathrun1.Add(new Vector3(1544.643f, 1440.352f, 0f));
            deathrun1.Add(new Vector3(1539.812f, 1441.415f, 0f));
            deathrun1.Add(new Vector3(1535.37f, 1441.706f, 0f));
            deathrun1.Add(new Vector3(1528.94f, 1442.06f, 0f));
            deathrun1.Add(new Vector3(1524.415f, 1442.309f, 0f));
            deathrun1.Add(new Vector3(1519.52f, 1442.579f, 0f));
            deathrun1.Add(new Vector3(1513.09f, 1442.938f, 0f), WaypointAction.Jump);
            deathrun1.Add(new Vector3(1508.161f, 1443.288f, 0f));
            deathrun1.Add(new Vector3(1503.792f, 1444.072f, 0f));
            deathrun1.Add(new Vector3(1499.637f, 1445.796f, 0f));
            deathrun1.Add(new Vector3(1495.666f, 1449.387f, 0f));
            deathrun1.Add(new Vector3(1493.858f, 1453.504f, 0f));
            deathrun1.Add(new Vector3(1493.407f, 1458.315f, 0f));
            deathrun1.Add(new Vector3(1493.335f, 1462.855f, 0f));
            deathrun1.Add(new Vector3(1493.265f, 1467.346f, 0f));
            deathrun1.Add(new Vector3(1493.188f, 1472.248f, 0f));
            deathrun1.Add(new Vector3(1493.11f, 1477.174f, 0f));
            deathrun1.Add(new Vector3(1493.033f, 1482.084f, 0f));
            deathrun1.Add(new Vector3(1492.939f, 1488.121f, 0f));
            deathrun1.Add(new Vector3(1492.888f, 1491.356f, 0f));
            deathrun1.Add(new Vector3(1492.815f, 1495.985f, 0f));
            deathrun1.Add(new Vector3(1492.691f, 1503.905f, 0f), WaypointAction.Jump);
            deathrun1.Add(new Vector3(1492.644f, 1506.857f, 0f));
            deathrun1.Add(new Vector3(1492.191f, 1511.758f, 0f));
            deathrun1.Add(new Vector3(1488.327f, 1516.803f, 0f));
            deathrun1.Add(new Vector3(1485.84f, 1518.996f, 0f));
            deathrun1.Add(new Vector3(1480.807f, 1522.469f, 0f));
            deathrun1.Add(new Vector3(1476.948f, 1524.751f, 0f), WaypointAction.Stop);
            //             deathrun1.Add(new Vector3(1472.911f, 1526.831f, 0f));
            //             deathrun1.Add(new Vector3(1470.136f, 1527.934f, 0f) WaypointAction.Stop);
            //             deathrun1.Add(new Vector3(1465.593f, 1529.145f, 0f));
            //             deathrun1.Add(new Vector3(1460.542f, 1529.475f, 0f));
            //             deathrun1.Add(new Vector3(1455.594f, 1528.183f, 0f));
            //             deathrun1.Add(new Vector3(1456.817f, 1528.94f, 0f));
            //             deathrun1.Add(new Vector3(1457.333f, 1527.012f, 0f),);


            basenoafk.Add(new Vector3(1463.317f, 1528.797f, 0f));
            basenoafk.Add(new Vector3(1465.125f, 1528.539f, 0f));
            basenoafk.Add(new Vector3(1469.041f, 1526.433f, 0f));
            basenoafk.Add(new Vector3(1470.181f, 1523.715f, 0f));
            basenoafk.Add(new Vector3(1469.552f, 1520.408f, 0f));
            basenoafk.Add(new Vector3(1467.943f, 1517.96f, 0f));
            basenoafk.Add(new Vector3(1465.4f, 1516.194f, 0f));
            basenoafk.Add(new Vector3(1462.893f, 1514.556f, 0f));
            basenoafk.Add(new Vector3(1460.854f, 1512.346f, 0f));
            basenoafk.Add(new Vector3(1459.546f, 1509.625f, 0f));
            basenoafk.Add(new Vector3(1459.147f, 1508.234f, 0f));
            basenoafk.Add(new Vector3(1458.662f, 1505.246f, 0f));
            basenoafk.Add(new Vector3(1458.176f, 1501.933f, 0f));
            basenoafk.Add(new Vector3(1457.972f, 1498.857f, 0f));
            basenoafk.Add(new Vector3(1457.869f, 1497.089f, 0f));
            basenoafk.Add(new Vector3(1457.677f, 1493.786f, 0f));
            basenoafk.Add(new Vector3(1457.518f, 1491.046f, 0f));
            basenoafk.Add(new Vector3(1457.343f, 1488.04f, 0f));
            basenoafk.Add(new Vector3(1457.083f, 1484.654f, 0f));
            basenoafk.Add(new Vector3(1456.729f, 1481.64f, 0f));
            basenoafk.Add(new Vector3(1456.31f, 1478.733f, 0f));
            basenoafk.Add(new Vector3(1455.902f, 1477.276f, 0f));
            basenoafk.Add(new Vector3(1454.44f, 1474.074f, 0f));
            basenoafk.Add(new Vector3(1453.468f, 1473f, 0f));
            basenoafk.Add(new Vector3(1450.988f, 1471.364f, 0f));
            basenoafk.Add(new Vector3(1448.063f, 1470.566f, 0f));
            basenoafk.Add(new Vector3(1445.046f, 1470.642f, 0f));
            basenoafk.Add(new Vector3(1442.099f, 1471.298f, 0f));
            basenoafk.Add(new Vector3(1439.123f, 1471.797f, 0f));
            basenoafk.Add(new Vector3(1436.264f, 1472.248f, 0f));
            basenoafk.Add(new Vector3(1433.147f, 1472.739f, 0f));
            basenoafk.Add(new Vector3(1431.302f, 1473.03f, 0f));
            basenoafk.Add(new Vector3(1428.348f, 1473.521f, 0f));
            basenoafk.Add(new Vector3(1425.399f, 1474.202f, 0f));
            basenoafk.Add(new Vector3(1422.34f, 1474.908f, 0f));
            basenoafk.Add(new Vector3(1419.407f, 1475.586f, 0f));
            basenoafk.Add(new Vector3(1416.446f, 1476.302f, 0f));
            basenoafk.Add(new Vector3(1413.578f, 1477.172f, 0f));
            basenoafk.Add(new Vector3(1411.767f, 1477.772f, 0f));
            basenoafk.Add(new Vector3(1408.952f, 1478.862f, 0f));
            basenoafk.Add(new Vector3(1406.236f, 1480.084f, 0f));
            basenoafk.Add(new Vector3(1403.342f, 1481.402f, 0f));
            basenoafk.Add(new Vector3(1400.58f, 1482.659f, 0f));
            basenoafk.Add(new Vector3(1397.888f, 1483.912f, 0f));
            basenoafk.Add(new Vector3(1396.561f, 1484.641f, 0f));
            basenoafk.Add(new Vector3(1393.749f, 1486.612f, 0f));
            basenoafk.Add(new Vector3(1391.574f, 1488.666f, 0f));
            basenoafk.Add(new Vector3(1389.66f, 1491f, 0f));
            basenoafk.Add(new Vector3(1387.715f, 1493.484f, 0f));
            basenoafk.Add(new Vector3(1386.832f, 1494.693f, 0f));
            basenoafk.Add(new Vector3(1385.419f, 1497.381f, 0f));
            basenoafk.Add(new Vector3(1384.668f, 1500.279f, 0f));
            basenoafk.Add(new Vector3(1384.701f, 1503.638f, 0f));
            basenoafk.Add(new Vector3(1385.582f, 1506.481f, 0f));
            basenoafk.Add(new Vector3(1387.184f, 1509.064f, 0f));
            basenoafk.Add(new Vector3(1388.263f, 1510.386f, 0f));
            basenoafk.Add(new Vector3(1390.288f, 1512.399f, 0f));
            basenoafk.Add(new Vector3(1393.064f, 1514.324f, 0f));
            basenoafk.Add(new Vector3(1395.685f, 1515.789f, 0f));
            basenoafk.Add(new Vector3(1398.465f, 1517.056f, 0f));
            basenoafk.Add(new Vector3(1401.344f, 1517.839f, 0f));
            basenoafk.Add(new Vector3(1402.774f, 1518.012f, 0f));
            basenoafk.Add(new Vector3(1406.059f, 1518.031f, 0f));
            basenoafk.Add(new Vector3(1408.812f, 1518.001f, 0f));
            basenoafk.Add(new Vector3(1411.952f, 1517.966f, 0f));
            basenoafk.Add(new Vector3(1415.018f, 1517.933f, 0f));
            basenoafk.Add(new Vector3(1418.222f, 1517.898f, 0f));
            basenoafk.Add(new Vector3(1421.088f, 1517.866f, 0f));
            basenoafk.Add(new Vector3(1424.031f, 1517.834f, 0f));
            basenoafk.Add(new Vector3(1425.585f, 1517.817f, 0f));
            basenoafk.Add(new Vector3(1428.555f, 1517.78f, 0f));
            basenoafk.Add(new Vector3(1431.677f, 1517.454f, 0f));
            basenoafk.Add(new Vector3(1434.767f, 1517.119f, 0f));
            basenoafk.Add(new Vector3(1437.778f, 1516.921f, 0f));
            basenoafk.Add(new Vector3(1440.758f, 1516.712f, 0f));
            basenoafk.Add(new Vector3(1443.744f, 1516.479f, 0f));
            basenoafk.Add(new Vector3(1445.199f, 1516.318f, 0f));
            basenoafk.Add(new Vector3(1448.24f, 1515.721f, 0f));
            basenoafk.Add(new Vector3(1451.254f, 1514.841f, 0f));
            basenoafk.Add(new Vector3(1452.594f, 1514.104f, 0f));
            basenoafk.Add(new Vector3(1455.008f, 1511.988f, 0f));
            basenoafk.Add(new Vector3(1456.333f, 1509.262f, 0f));
            basenoafk.Add(new Vector3(1456.885f, 1506.328f, 0f));
            basenoafk.Add(new Vector3(1457.23f, 1503.283f, 0f));
            basenoafk.Add(new Vector3(1457.493f, 1499.969f, 0f));
            basenoafk.Add(new Vector3(1457.682f, 1496.931f, 0f));
            basenoafk.Add(new Vector3(1457.7f, 1495.466f, 0f));
            basenoafk.Add(new Vector3(1457.739f, 1492.391f, 0f));
            basenoafk.Add(new Vector3(1457.782f, 1489.002f, 0f));
            basenoafk.Add(new Vector3(1457.82f, 1485.975f, 0f));
            basenoafk.Add(new Vector3(1457.994f, 1483.036f, 0f));
            basenoafk.Add(new Vector3(1459.337f, 1480.177f, 0f));
            basenoafk.Add(new Vector3(1461.516f, 1478.135f, 0f));
            basenoafk.Add(new Vector3(1464.262f, 1476.741f, 0f));
            basenoafk.Add(new Vector3(1467.211f, 1476.122f, 0f));
            basenoafk.Add(new Vector3(1469.027f, 1475.858f, 0f));
            basenoafk.Add(new Vector3(1472.011f, 1475.54f, 0f));
            basenoafk.Add(new Vector3(1474.98f, 1475.624f, 0f));
            basenoafk.Add(new Vector3(1478.077f, 1475.736f, 0f));
            basenoafk.Add(new Vector3(1481.068f, 1476.062f, 0f));
            basenoafk.Add(new Vector3(1484.016f, 1476.701f, 0f));
            basenoafk.Add(new Vector3(1486.939f, 1477.486f, 0f));
            basenoafk.Add(new Vector3(1489.775f, 1478.396f, 0f));
            basenoafk.Add(new Vector3(1492.072f, 1480.256f, 0f));
            basenoafk.Add(new Vector3(1493.641f, 1482.822f, 0f));
            basenoafk.Add(new Vector3(1494.786f, 1485.656f, 0f));
            basenoafk.Add(new Vector3(1495.169f, 1488.687f, 0f));
            basenoafk.Add(new Vector3(1494.681f, 1492.041f, 0f));
            basenoafk.Add(new Vector3(1494.082f, 1493.403f, 0f));
            basenoafk.Add(new Vector3(1492.146f, 1495.713f, 0f));
            basenoafk.Add(new Vector3(1488.23f, 1497.775f, 0f));
            basenoafk.Add(new Vector3(1486.347f, 1498.33f, 0f));
            basenoafk.Add(new Vector3(1483.424f, 1498.823f, 0f));
            basenoafk.Add(new Vector3(1480.382f, 1498.882f, 0f));
            basenoafk.Add(new Vector3(1477.387f, 1498.938f, 0f));
            basenoafk.Add(new Vector3(1473.927f, 1499.004f, 0f));
            basenoafk.Add(new Vector3(1472.47f, 1499.036f, 0f));
            basenoafk.Add(new Vector3(1469.38f, 1499.935f, 0f));
            basenoafk.Add(new Vector3(1467.051f, 1501.852f, 0f));
            basenoafk.Add(new Vector3(1465.369f, 1504.313f, 0f));
            basenoafk.Add(new Vector3(1464.023f, 1507.03f, 0f));
            basenoafk.Add(new Vector3(1463.082f, 1509.976f, 0f));
            basenoafk.Add(new Vector3(1462.301f, 1512.825f, 0f));
            basenoafk.Add(new Vector3(1461.542f, 1515.738f, 0f));
            basenoafk.Add(new Vector3(1460.645f, 1518.669f, 0f));
            basenoafk.Add(new Vector3(1458.551f, 1521.243f, 0f));
            basenoafk.Add(new Vector3(1456.199f, 1523.125f, 0f));
            basenoafk.Add(new Vector3(1454.096f, 1527.075f, 0f));
            basenoafk.Add(new Vector3(1452.52f, 1530.075f, 0f));
            basenoafk.Add(new Vector3(1455.6f, 1531.878f, 0f));
            basenoafk.Add(new Vector3(1458.184f, 1530.572f, 0f));
            basenoafk.Add(new Vector3(1459.516f, 1527.902f, 0f));
            basenoafk.Add(new Vector3(1459.231f, 1525.999f, 0f));
            basenoafk.Add(new Vector3(1458.428f, 1523.223f, 0f));
            basenoafk.Add(new Vector3(1457.917f, 1520.14f, 0f));
            basenoafk.Add(new Vector3(1457.992f, 1517.144f, 0f));
            basenoafk.Add(new Vector3(1457.969f, 1514.175f, 0f));
            basenoafk.Add(new Vector3(1457.886f, 1511.053f, 0f));
            basenoafk.Add(new Vector3(1457.805f, 1508.019f, 0f));
            basenoafk.Add(new Vector3(1457.724f, 1504.986f, 0f));
            basenoafk.Add(new Vector3(1457.603f, 1500.459f, 0f));
            basenoafk.Add(new Vector3(1457.513f, 1497.055f, 0f));
            basenoafk.Add(new Vector3(1457.432f, 1494.038f, 0f));
            basenoafk.Add(new Vector3(1457.344f, 1490.722f, 0f));
            basenoafk.Add(new Vector3(1457.262f, 1487.664f, 0f));
            basenoafk.Add(new Vector3(1457.171f, 1484.236f, 0f));
            basenoafk.Add(new Vector3(1457.274f, 1481.28f, 0f));
            basenoafk.Add(new Vector3(1457.411f, 1479.781f, 0f));
            basenoafk.Add(new Vector3(1457.727f, 1476.318f, 0f));
            basenoafk.Add(new Vector3(1458.043f, 1473.032f, 0f));
            basenoafk.Add(new Vector3(1458.358f, 1469.982f, 0f));
            basenoafk.Add(new Vector3(1458.946f, 1466.617f, 0f));
//             basenoafk.Add(new Vector3(1460.188f, 1463.893f, 0f));
//             basenoafk.Add(new Vector3(1461.493f, 1461.207f, 0f));
//             basenoafk.Add(new Vector3(1462.461f, 1459.936f, 0f));
//             basenoafk.Add(new Vector3(1464.493f, 1457.974f, 0f));
//             basenoafk.Add(new Vector3(1466.089f, 1457.244f, 0f));
            basenoafk.Add(new Vector3(1460.813f, 1454.658f, 0f));
            basenoafk.Add(new Vector3(1459.71f, 1453.065f, 0f));
            basenoafk.Add(new Vector3(1463.216f, 1451.299f, 0f));

            basenoafk.Add(new Vector3(1474.957f, 1455.557f, 0f));
            basenoafk.Add(new Vector3(1478.367f, 1455.26f, 0f));
            basenoafk.Add(new Vector3(1481.346f, 1455.613f, 0f));
            basenoafk.Add(new Vector3(1491.947f, 1453.075f, 0f));
            basenoafk.Add(new Vector3(1484.351f, 1456.14f, 0f));
            basenoafk.Add(new Vector3(1485.729f, 1456.515f, 0f));
            basenoafk.Add(new Vector3(1490.141f, 1458.742f, 0f));
            basenoafk.Add(new Vector3(1491.453f, 1459.88f, 0f));
            basenoafk.Add(new Vector3(1493.214f, 1462.661f, 0f));
            basenoafk.Add(new Vector3(1494.089f, 1465.214f, 0f));
            basenoafk.Add(new Vector3(1494.462f, 1468.307f, 0f));
            basenoafk.Add(new Vector3(1494.676f, 1471.315f, 0f));
            basenoafk.Add(new Vector3(1494.674f, 1474.559f, 0f));
            basenoafk.Add(new Vector3(1494.671f, 1477.393f, 0f));
            basenoafk.Add(new Vector3(1494.668f, 1480.806f, 0f));
            basenoafk.Add(new Vector3(1494.666f, 1483.857f, 0f));
            basenoafk.Add(new Vector3(1494.664f, 1486.827f, 0f));
            basenoafk.Add(new Vector3(1494.661f, 1489.878f, 0f));
            basenoafk.Add(new Vector3(1494.656f, 1496.326f, 0f));
            basenoafk.Add(new Vector3(1494.653f, 1499.305f, 0f));
            basenoafk.Add(new Vector3(1494.652f, 1500.818f, 0f));
            basenoafk.Add(new Vector3(1493.959f, 1504.137f, 0f));
            basenoafk.Add(new Vector3(1492.783f, 1506.902f, 0f));
            basenoafk.Add(new Vector3(1491.534f, 1508.523f, 0f));
            basenoafk.Add(new Vector3(1489.852f, 1510.339f, 0f));
            basenoafk.Add(new Vector3(1488.809f, 1511.411f, 0f));
            basenoafk.Add(new Vector3(1487.487f, 1512.477f, 0f));
            basenoafk.Add(new Vector3(1486.299f, 1513.435f, 0f));
            basenoafk.Add(new Vector3(1483.815f, 1515.438f, 0f));
            basenoafk.Add(new Vector3(1476.621f, 1521.239f, 0f));
            basenoafk.Add(new Vector3(1475.869f, 1521.845f, 0f));
            basenoafk.Add(new Vector3(1474.706f, 1522.782f, 0f));
            basenoafk.Add(new Vector3(1472.14f, 1524.851f, 0f));
            basenoafk.Add(new Vector3(1469.771f, 1526.761f, 0f));
            basenoafk.Add(new Vector3(1466.684f, 1528.192f, 0f));






        }

        public void onKeyEvent(Object o, int key)
        {

            if (key == 0x75) // F6
            {
                theform.Enable();
                Enabledcheck.Checked = true;
                theform.moveclick();
                wasmoving = true;

                WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                if (plyr != null)
                {
                    currentwaypoint = waypoints.FindClosestTo(new Vector3(plyr.X, plyr.Y, plyr.Z));
                }

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
                    theform.AddWaypointText("waypoints.Add(new Vector3(" + plyr.X.ToString() + "f, " + plyr.Y.ToString() + "f, " + " 0f));");
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
        string prevloc = "";

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
                        waypoints = startrun;
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
                        theform.joinWSG();
                        Thread.Sleep(8000);

                        wasnotinbg = true;
                        Thread.Sleep(66);
                        continue;
                    }
                    if(wasnotinbg == true)
                    {
                        wasnotinbg = false;
                        if (wasmoving == false)
                        {
                            wasmoving = true;
                            theform.moveclick();
                            theform.moveclick();
                            theform.moveclick();
                            theform.moveclick();
                            theform.moveclick();
                            theform.moveclick();
                        }
                    }
                    Vector3 targetpos;
                    if (prevloc != "" &&  prevloc != "battleground" && env.Location == "battleground")
                    {
                        waypoints = startrun;
                        currentwaypoint = waypoints.Get(0);
                        targetpos = currentwaypoint.location;
                    }
                    prevloc = env.Location;


                    WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                    if (plyr != null)
                    {

                        //theform.moveclick();
                        ++count;

                        // determine forward vector
                        float xx = (float)Math.Cos(plyr.Facing + (Math.PI / 2));
                        float yy = (float)Math.Sin(plyr.Facing + (Math.PI / 2));
                        Vector3 forward = new Vector3(xx, yy, 0);
                        Vector3 mypos = new Vector3(plyr.X, plyr.Y, 0);

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
                            currentwaypoint = waypoints.FindClosestTo(mypos);
                            targetpos = currentwaypoint.location;
                        }
                        else if (wasdead)
                        {
                            theform.moveclick();
                            wasdead = false;
                            waypoints = deathrun1;
                            currentwaypoint = waypoints.Get(0);
                            targetpos = currentwaypoint.location;
                        }
                        else
                        {
                            if (wasfighting)
                            {
                                wasfighting = false;
                                // Find the closest waypoint of our list, and run there
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
                                if (waypoints.Name == startrun.Name)
                                {
                                    waypoints = hordecircle;
                                    currentwaypoint = waypoints.Get(0);
                                    targetpos = currentwaypoint.location;

                                }
                                else if (waypoints.Name == deathrun1.Name)
                                {
                                    waypoints = basenoafk;
                                    currentwaypoint = waypoints.Get(0);
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

//                         if (offset.X > 0.01)//&& offset.Y > 0)
//                         {
//                             // go left
//                             Point oldpos = Cursor.Position;
//                             //                         if (offset.Y <= 0)
//                             //                             Cursor.Position = new Point(oldpos.X + gain*(int)(offset.X*200), oldpos.Y);
//                             //                         else
//                             Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
//                         }
//                         else if (offset.X < -0.01)// && offset.Y > 0)
//                         {
//                             // go right
//                             Point oldpos = Cursor.Position;
//                             Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
//                         }
//                         //                     else if (offset.X > -0.01 && offset.X < 0.01 /*&& offset.Y > 0*/)
//                         //                     {
//                         //                         // Should be runnung right at it
//                         // 
//                         //                     }
// 
//                         else if (offset.Y <= 0)
//                         {
//                             // go left
//                             Point oldpos = Cursor.Position;
//                             //                         if (offset.Y <= 0)
//                             //                             Cursor.Position = new Point(oldpos.X + gain*(int)(offset.X*200), oldpos.Y);
//                             //                         else
//                             Cursor.Position = new Point(oldpos.X + gain * (int)(offset.X * 10), oldpos.Y);
//                         }


                        // }
                    }
                }
                catch (System.Exception ex)
                {

                }
                Thread.Sleep(16);
            }
        }

        private void FindBobber()
        {
            //int mouseid = env.Memory.ReadInteger(MOUSECURSOR);
            // if (mousefound) return;
            for (int i = 618; i > 150; i -= 20)
            {
                //if (mousefound) break;
                for (int j = 824; j > 200; j -= 20)
                {
                    //                     if (mousefound) break;
                    //                     mouseid = env.Memory.ReadInteger(MOUSECURSOR);
                    //                     if (mouseid == 5)
                    //                     {
                    //                         mousefound = true;
                    //                         break;
                    //                     }

                    Cursor.Position = new Point(j, i);

                    Thread.Sleep(5);

                }
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
