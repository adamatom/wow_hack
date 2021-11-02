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
    class SOTAMovementBot : WoahBotBase
    {
        public MenuItem Enabledcheck;


        Waypoint currentwaypoint = new Waypoint();
        WaypointList BeginningWait = new WaypointList("BeginningWait");
        WaypointList LeftToBegGY = new WaypointList("LeftToBegGY");
        WaypointList RightToBegGY = new WaypointList("RightToBegGY");
        WaypointList BegGYToLeftMid = new WaypointList("BegGYToLeftMid");
        WaypointList LeftMidToRightMid = new WaypointList("LeftMidToRightMid");
        WaypointList RightMidToUpper = new WaypointList("RightMidToUpper");
        WaypointList DefenseStartToUpper = new WaypointList("DefenseStartToUpper");
        WaypointList DefenseUpper = new WaypointList("DefenseUpper");
        WaypointList DefenseTop = new WaypointList("DefenseTop");
        WaypointList GetOffBoat = new WaypointList("GetOffBoat");

        DateTime dt = new DateTime();
        bool waiting = false;

        WaypointList waypoints = null;

        public SOTAMovementBot(WoahEnvironment env, WoahFish theform)
            : base(env, theform)
        {
            theform.HookKeyPress += new myKeyEventHandler(onKeyEvent);
            BeginningWait.Add(new Vector3(0f, 0f, 0f), WaypointAction.Wait2Min);
            waypoints = BeginningWait;

            LeftToBegGY.Add(new Vector3(1.325438f, 4.644502f, 9.454456f));
            LeftToBegGY.Add(new Vector3(1.325438f, 4.644502f, 9.454456f));
            LeftToBegGY.Add(new Vector3(4.026153f, 4.793106f, 9.433872f));
            LeftToBegGY.Add(new Vector3(8.068837f, 5.015551f, 7.763711f));
            LeftToBegGY.Add(new Vector3(12.16185f, 5.240766f, 5.344203f));
            LeftToBegGY.Add(new Vector3(46.8339f, 1611.723f, 7.579743f));
            LeftToBegGY.Add(new Vector3(50.10762f, 1608.656f, 7.579743f));
            LeftToBegGY.Add(new Vector3(53.12384f, 1605.831f, 7.724774f));
            LeftToBegGY.Add(new Vector3(56.29947f, 1602.856f, 7.952053f));
            LeftToBegGY.Add(new Vector3(59.24213f, 1600.1f, 7.82364f));
            LeftToBegGY.Add(new Vector3(62.01314f, 1597.504f, 7.67082f));
            LeftToBegGY.Add(new Vector3(65.16425f, 1594.552f, 7.690076f));
            LeftToBegGY.Add(new Vector3(67.81264f, 1592.072f, 7.813373f));
            LeftToBegGY.Add(new Vector3(70.82582f, 1589.173f, 7.940971f));
            LeftToBegGY.Add(new Vector3(73.39126f, 1586.062f, 7.010071f));
            LeftToBegGY.Add(new Vector3(75.66484f, 1583.299f, 6.360958f));
            LeftToBegGY.Add(new Vector3(78.37106f, 1579.769f, 5.792323f));
            LeftToBegGY.Add(new Vector3(80.62591f, 1576.346f, 4.999742f));
            LeftToBegGY.Add(new Vector3(82.95126f, 1572.748f, 4.555743f));
            LeftToBegGY.Add(new Vector3(84.10667f, 1569.446f, 3.974046f));
            LeftToBegGY.Add(new Vector3(82.78843f, 1565.293f, 4.364706f));
            LeftToBegGY.Add(new Vector3(81.1853f, 1562.113f, 4.56271f));
            LeftToBegGY.Add(new Vector3(78.14139f, 1556.235f, 5.212266f));
            LeftToBegGY.Add(new Vector3(74.99704f, 1550.163f, 5.422f));
            LeftToBegGY.Add(new Vector3(73.01927f, 1546.344f, 5.458417f));
            LeftToBegGY.Add(new Vector3(70.99515f, 1542.436f, 5.515306f));
            LeftToBegGY.Add(new Vector3(68.88945f, 1538.38f, 5.531317f));
            LeftToBegGY.Add(new Vector3(66.50153f, 1534.724f, 5.534098f));
            LeftToBegGY.Add(new Vector3(63.62265f, 1530.909f, 5.539736f));
            LeftToBegGY.Add(new Vector3(60.23357f, 1528.73f, 5.539736f));
            LeftToBegGY.Add(new Vector3(56.24696f, 1526.429f, 5.535811f));
            LeftToBegGY.Add(new Vector3(52.46405f, 1524.245f, 5.520286f));
            LeftToBegGY.Add(new Vector3(49.01578f, 1522.255f, 5.487071f));
            LeftToBegGY.Add(new Vector3(45.62571f, 1520.297f, 5.475644f));
            LeftToBegGY.Add(new Vector3(40.86796f, 1517.551f, 5.299765f));
            LeftToBegGY.Add(new Vector3(33.79682f, 1513.469f, 4.955144f));
            LeftToBegGY.Add(new Vector3(30.49405f, 1511.562f, 4.832175f));
            LeftToBegGY.Add(new Vector3(26.69658f, 1509.37f, 4.785516f));
            LeftToBegGY.Add(new Vector3(23.14646f, 1507.321f, 4.745192f));
            LeftToBegGY.Add(new Vector3(16.46816f, 1503.465f, 4.765491f));
            LeftToBegGY.Add(new Vector3(12.21966f, 1501.013f, 5.066111f));
            LeftToBegGY.Add(new Vector3(4.435581f, 1496.519f, 5.483145f));
            LeftToBegGY.Add(new Vector3(0.8418121f, 1494.445f, 5.537206f));
            LeftToBegGY.Add(new Vector3(-2.664654f, 1492.194f, 5.54409f));
            LeftToBegGY.Add(new Vector3(-6.640115f, 1489.848f, 5.625494f));
            LeftToBegGY.Add(new Vector3(-9.714513f, 1487.68f, 6.166667f));
            LeftToBegGY.Add(new Vector3(-12.69573f, 1485.212f, 6.338887f));
            LeftToBegGY.Add(new Vector3(-16.11822f, 1482.159f, 6.38023f));
            LeftToBegGY.Add(new Vector3(-19.35576f, 1479.252f, 6.317855f));
            LeftToBegGY.Add(new Vector3(-22.38079f, 1476.535f, 6.180926f));
            LeftToBegGY.Add(new Vector3(-25.74333f, 1473.516f, 5.944969f));
            LeftToBegGY.Add(new Vector3(-28.78086f, 1470.788f, 5.717506f));
            LeftToBegGY.Add(new Vector3(-31.8809f, 1468.005f, 5.480649f));
            LeftToBegGY.Add(new Vector3(-35.09343f, 1465.12f, 5.350984f));
            LeftToBegGY.Add(new Vector3(-38.04347f, 1462.471f, 5.15261f));
            LeftToBegGY.Add(new Vector3(-40.981f, 1459.833f, 4.730268f));
            LeftToBegGY.Add(new Vector3(-43.78104f, 1457.319f, 4.81072f));
            LeftToBegGY.Add(new Vector3(-46.89357f, 1454.524f, 5.142165f));
            LeftToBegGY.Add(new Vector3(-49.71861f, 1451.988f, 5.448565f), WaypointAction.Stop);

            RightToBegGY.Add(new Vector3(-60.56664f, 1625.756f, -2.185715f));
            RightToBegGY.Add(new Vector3(-60.11802f, 1626.598f, -2.185715f));
            RightToBegGY.Add(new Vector3(-59.55135f, 1627.267f, -2.185715f));
            RightToBegGY.Add(new Vector3(-58.73189f, 1628.036f, -2.185715f));
            RightToBegGY.Add(new Vector3(-58.07713f, 1628.498f, -2.185715f));
            RightToBegGY.Add(new Vector3(-57.25893f, 1628.969f, -2.185715f));
            RightToBegGY.Add(new Vector3(-56.43242f, 1629.373f, -2.185715f));
            RightToBegGY.Add(new Vector3(-55.49582f, 1629.652f, -2.185715f));
            RightToBegGY.Add(new Vector3(-54.6164f, 1629.909f, -2.185715f));
            RightToBegGY.Add(new Vector3(-53.61459f, 1630.202f, -2.185715f));
            RightToBegGY.Add(new Vector3(-52.78049f, 1630.445f, -2.185715f));
            RightToBegGY.Add(new Vector3(-51.80471f, 1630.707f, -2.185715f));
            RightToBegGY.Add(new Vector3(-50.93012f, 1630.907f, -2.185715f));
            RightToBegGY.Add(new Vector3(-50.04632f, 1631.109f, -2.185715f));
            RightToBegGY.Add(new Vector3(-49.01522f, 1631.345f, -2.185715f));
            RightToBegGY.Add(new Vector3(-47.97031f, 1631.584f, -2.185715f));
            RightToBegGY.Add(new Vector3(-46.98985f, 1631.809f, -2.185715f));
            RightToBegGY.Add(new Vector3(-45.74701f, 1632.094f, -2.185715f));
            RightToBegGY.Add(new Vector3(-44.79416f, 1632.312f, -2.185715f));
            RightToBegGY.Add(new Vector3(-43.52831f, 1632.601f, -2.185715f));
            RightToBegGY.Add(new Vector3(-42.57546f, 1632.82f, -2.185715f));
            RightToBegGY.Add(new Vector3(-41.50703f, 1633.007f, -2.185715f));
            RightToBegGY.Add(new Vector3(-40.44267f, 1633.134f, -2.185715f));
            RightToBegGY.Add(new Vector3(-39.3877f, 1633.261f, -2.185715f));
            RightToBegGY.Add(new Vector3(-38.44993f, 1633.373f, -2.185715f));
            RightToBegGY.Add(new Vector3(-37.48622f, 1633.462f, -2.185715f));
            RightToBegGY.Add(new Vector3(-36.56186f, 1633.509f, -2.185715f));
            RightToBegGY.Add(new Vector3(-35.41998f, 1633.551f, -2.185715f));
            RightToBegGY.Add(new Vector3(-34.38852f, 1633.493f, -2.185715f));
            RightToBegGY.Add(new Vector3(-33.48823f, 1633.352f, -2.185715f));
            RightToBegGY.Add(new Vector3(-32.4658f, 1633.168f, -2.185715f));
            RightToBegGY.Add(new Vector3(-31.50399f, 1632.993f, -2.185715f));
            RightToBegGY.Add(new Vector3(-30.58399f, 1632.826f, -2.185715f));
            RightToBegGY.Add(new Vector3(-29.64235f, 1632.625f, -2.185715f));
            RightToBegGY.Add(new Vector3(-28.64046f, 1632.317f, -2.185715f));
            RightToBegGY.Add(new Vector3(-27.77566f, 1632.044f, -2.185715f));
            RightToBegGY.Add(new Vector3(-26.84049f, 1631.744f, -2.185715f));
            RightToBegGY.Add(new Vector3(-25.90198f, 1631.383f, -2.185715f));
            RightToBegGY.Add(new Vector3(-25.01789f, 1630.977f, -2.185715f));
            RightToBegGY.Add(new Vector3(-24.24122f, 1630.609f, -2.185715f));
            RightToBegGY.Add(new Vector3(-23.35135f, 1630.131f, -2.185715f));
            RightToBegGY.Add(new Vector3(-22.52865f, 1629.621f, -2.185715f));
            RightToBegGY.Add(new Vector3(-21.69617f, 1629.04f, -2.185715f));
            RightToBegGY.Add(new Vector3(-20.96534f, 1628.528f, -2.185715f));
            RightToBegGY.Add(new Vector3(-20.09143f, 1627.915f, -2.185715f));
            RightToBegGY.Add(new Vector3(-19.35021f, 1627.393f, -2.185715f));
            RightToBegGY.Add(new Vector3(-18.62557f, 1626.759f, -2.185715f));
            RightToBegGY.Add(new Vector3(-17.90876f, 1626.108f, -2.185715f));
            RightToBegGY.Add(new Vector3(-17.14488f, 1625.411f, -2.185715f));
            RightToBegGY.Add(new Vector3(-16.40693f, 1624.595f, -2.185715f));
            RightToBegGY.Add(new Vector3(-15.79991f, 1623.835f, -2.185715f));
            RightToBegGY.Add(new Vector3(-15.1922f, 1623.063f, -2.185715f));
            RightToBegGY.Add(new Vector3(-14.67135f, 1622.154f, -2.185715f));
            RightToBegGY.Add(new Vector3(-14.25328f, 1621.286f, -2.185715f));
            RightToBegGY.Add(new Vector3(-13.78602f, 1620.316f, -2.185715f));
            RightToBegGY.Add(new Vector3(-13.33516f, 1619.38f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.96012f, 1618.602f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.52493f, 1617.644f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.19765f, 1616.713f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.86953f, 1615.707f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.61887f, 1614.777f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.46191f, 1613.908f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.29488f, 1612.826f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.19926f, 1611.953f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.08645f, 1610.859f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.0182f, 1610.068f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.9403f, 1609.094f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.87782f, 1608.085f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.85178f, 1607.179f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.82829f, 1606.225f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.84255f, 1605.201f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.858f, 1604.134f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.89958f, 1603.101f, -2.185715f));
            RightToBegGY.Add(new Vector3(-10.94919f, 1602.2f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.00826f, 1601.177f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.07933f, 1600.108f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.18281f, 1599.093f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.30221f, 1598.213f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.44594f, 1597.222f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.60988f, 1596.254f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.83493f, 1595.067f, -2.185715f));
            RightToBegGY.Add(new Vector3(-11.97296f, 1594.338f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.17888f, 1593.354f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.41815f, 1592.318f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.63108f, 1591.525f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.91318f, 1590.535f, -2.185715f));
            RightToBegGY.Add(new Vector3(-12.93464f, 1589.82f, -2.084502f));
            RightToBegGY.Add(new Vector3(-12.77668f, 1588.747f, -4.055357f));
            RightToBegGY.Add(new Vector3(-13.02845f, 1587.775f, -4.153653f));
            RightToBegGY.Add(new Vector3(-13.3105f, 1586.8f, -4.153653f));
            RightToBegGY.Add(new Vector3(-13.59517f, 1585.816f, -4.153653f));
            RightToBegGY.Add(new Vector3(-13.83787f, 1584.976f, -4.153653f));
            RightToBegGY.Add(new Vector3(-13.92842f, 1584.015f, -4.223098f));
            RightToBegGY.Add(new Vector3(-13.8667f, 1582.256f, -4.02794f));
            RightToBegGY.Add(new Vector3(-13.83491f, 1581.387f, -3.448252f));
            RightToBegGY.Add(new Vector3(-13.8185f, 1580.938f, -3.004524f));
            RightToBegGY.Add(new Vector3(-13.77426f, 1579.729f, -3.100799f));
            RightToBegGY.Add(new Vector3(-13.56057f, 1577.103f, -1.087775f));
            RightToBegGY.Add(new Vector3(-13.28718f, 1575.315f, -0.2635346f));
            RightToBegGY.Add(new Vector3(-11.88392f, 1571.571f, -0.2635346f));
            RightToBegGY.Add(new Vector3(-10.62807f, 1568.22f, -0.2635346f));
            RightToBegGY.Add(new Vector3(-9.419375f, 1564.995f, -0.2635346f));
            RightToBegGY.Add(new Vector3(-8.415977f, 1561.2f, -0.2635346f));
            RightToBegGY.Add(new Vector3(-7.703594f, 1558.089f, -0.2635346f));
            RightToBegGY.Add(new Vector3(-6.799991f, 1554.142f, -0.1924542f));
            RightToBegGY.Add(new Vector3(-6.104361f, 1551.113f, 0.01948575f));
            RightToBegGY.Add(new Vector3(-5.30349f, 1547.677f, 0.2165982f));
            RightToBegGY.Add(new Vector3(-4.798093f, 1544.252f, 0.365604f));
            RightToBegGY.Add(new Vector3(-4.763279f, 1540.421f, 0.4832731f));
            RightToBegGY.Add(new Vector3(-4.729076f, 1536.658f, 0.556868f));
            RightToBegGY.Add(new Vector3(-4.699607f, 1533.416f, 0.5949923f));
            RightToBegGY.Add(new Vector3(-4.850676f, 1529.964f, 0.6137739f));
            RightToBegGY.Add(new Vector3(-5.247257f, 1526.458f, 0.6202366f));
            RightToBegGY.Add(new Vector3(-6.001343f, 1523.171f, 0.6300769f));
            RightToBegGY.Add(new Vector3(-7.63152f, 1520.023f, 0.7137688f));
            RightToBegGY.Add(new Vector3(-9.122629f, 1517.144f, 0.9171919f));
            RightToBegGY.Add(new Vector3(-10.74508f, 1514.011f, 1.386294f));
            RightToBegGY.Add(new Vector3(-12.39843f, 1510.819f, 2.114827f));
            RightToBegGY.Add(new Vector3(-13.905f, 1507.91f, 2.940151f));
            RightToBegGY.Add(new Vector3(-15.52745f, 1504.777f, 3.928747f));
            RightToBegGY.Add(new Vector3(-17.14217f, 1501.659f, 4.698697f));
            RightToBegGY.Add(new Vector3(-18.84188f, 1498.377f, 5.228761f));
            RightToBegGY.Add(new Vector3(-20.44888f, 1495.274f, 5.519115f));
            RightToBegGY.Add(new Vector3(-22.18722f, 1491.917f, 5.847156f));
            RightToBegGY.Add(new Vector3(-23.85603f, 1488.695f, 6.174909f));
            RightToBegGY.Add(new Vector3(-25.70554f, 1485.124f, 6.373411f));
            RightToBegGY.Add(new Vector3(-27.45934f, 1481.737f, 6.379336f));
            RightToBegGY.Add(new Vector3(-29.25948f, 1478.261f, 6.219335f));
            RightToBegGY.Add(new Vector3(-31.07508f, 1474.756f, 5.987558f));
            RightToBegGY.Add(new Vector3(-32.84433f, 1471.339f, 5.776736f));
            RightToBegGY.Add(new Vector3(-34.55949f, 1468.028f, 5.552008f));
            RightToBegGY.Add(new Vector3(-36.4447f, 1464.698f, 5.3389f));
            RightToBegGY.Add(new Vector3(-38.35973f, 1461.896f, 5.046162f));
            RightToBegGY.Add(new Vector3(-40.83076f, 1459.33f, 4.946669f));
            RightToBegGY.Add(new Vector3(-43.45583f, 1457.152f, 4.946669f));
            RightToBegGY.Add(new Vector3(-46.31367f, 1454.782f, 5.096496f));
            RightToBegGY.Add(new Vector3(-48.81559f, 1452.747f, 5.342943f));
            RightToBegGY.Add(new Vector3(-51.13642f, 1450.942f, 5.561223f), WaypointAction.Stop);

            BegGYToLeftMid.Add(new Vector3(-47.59889f, 1454.683f, 5.17791f));
            BegGYToLeftMid.Add(new Vector3(-48.34264f, 1457.873f, 5.071797f));
            BegGYToLeftMid.Add(new Vector3(-48.03164f, 1461.793f, 5.817639f));
            BegGYToLeftMid.Add(new Vector3(-46.01265f, 1464.989f, 5.833314f));
            BegGYToLeftMid.Add(new Vector3(-43.52346f, 1468.384f, 5.836339f));
            BegGYToLeftMid.Add(new Vector3(-40.2992f, 1471.197f, 5.838689f));
            BegGYToLeftMid.Add(new Vector3(-35.45961f, 1473.596f, 5.845578f));
            BegGYToLeftMid.Add(new Vector3(-31.78685f, 1475.218f, 6.001235f));
            BegGYToLeftMid.Add(new Vector3(-27.80346f, 1476.35f, 6.130653f));
            BegGYToLeftMid.Add(new Vector3(-23.82444f, 1477.097f, 6.201416f));
            BegGYToLeftMid.Add(new Vector3(-19.79065f, 1477.47f, 6.231539f));
            BegGYToLeftMid.Add(new Vector3(-15.72636f, 1477.549f, 6.211885f));
            BegGYToLeftMid.Add(new Vector3(-11.54322f, 1477.571f, 6.129947f));
            BegGYToLeftMid.Add(new Vector3(-7.363088f, 1477.434f, 5.978221f));
            BegGYToLeftMid.Add(new Vector3(-3.167649f, 1477.239f, 5.798024f));
            BegGYToLeftMid.Add(new Vector3(0.9540207f, 1476.938f, 5.686013f));
            BegGYToLeftMid.Add(new Vector3(4.992968f, 1476.346f, 5.830064f));
            BegGYToLeftMid.Add(new Vector3(9.609597f, 1475.639f, 6.22003f));
            BegGYToLeftMid.Add(new Vector3(13.19932f, 1474.933f, 6.302596f));
            BegGYToLeftMid.Add(new Vector3(17.03297f, 1473.922f, 6.040009f));
            BegGYToLeftMid.Add(new Vector3(20.57423f, 1472.987f, 5.98345f));
            BegGYToLeftMid.Add(new Vector3(24.14798f, 1472.045f, 6.176659f));
            BegGYToLeftMid.Add(new Vector3(27.62906f, 1471.025f, 5.941679f));
            BegGYToLeftMid.Add(new Vector3(31.32903f, 1469.747f, 5.642731f));
            BegGYToLeftMid.Add(new Vector3(34.86407f, 1468.459f, 5.559996f));
            BegGYToLeftMid.Add(new Vector3(38.53155f, 1466.828f, 5.614441f));
            BegGYToLeftMid.Add(new Vector3(42.13832f, 1464.88f, 5.696279f));
            BegGYToLeftMid.Add(new Vector3(45.27628f, 1463.025f, 5.666503f));
            BegGYToLeftMid.Add(new Vector3(49.10892f, 1460.663f, 6.076031f));
            BegGYToLeftMid.Add(new Vector3(52.55545f, 1458.538f, 6.999066f));
            BegGYToLeftMid.Add(new Vector3(56.05917f, 1456.378f, 8.116708f));
            BegGYToLeftMid.Add(new Vector3(59.41988f, 1454.306f, 9.062524f));
            BegGYToLeftMid.Add(new Vector3(62.75767f, 1452.249f, 10.06577f));
            BegGYToLeftMid.Add(new Vector3(66.51881f, 1449.93f, 11.27261f));
            BegGYToLeftMid.Add(new Vector3(70.19415f, 1447.664f, 12.44285f));
            BegGYToLeftMid.Add(new Vector3(73.98554f, 1445.269f, 13.77711f));
            BegGYToLeftMid.Add(new Vector3(77.65204f, 1442.656f, 15.2228f));
            BegGYToLeftMid.Add(new Vector3(80.60758f, 1440.49f, 16.49216f));
            BegGYToLeftMid.Add(new Vector3(84.00304f, 1437.906f, 18.08866f));
            BegGYToLeftMid.Add(new Vector3(87.24713f, 1435.295f, 19.77799f));
            BegGYToLeftMid.Add(new Vector3(93.74935f, 1429.486f, 23.40943f));
            BegGYToLeftMid.Add(new Vector3(96.91718f, 1426.643f, 25.08017f));
            BegGYToLeftMid.Add(new Vector3(100.0161f, 1423.808f, 26.6367f));
            BegGYToLeftMid.Add(new Vector3(103.0804f, 1420.986f, 27.91697f));
            BegGYToLeftMid.Add(new Vector3(106.5626f, 1417.724f, 28.69209f));
            BegGYToLeftMid.Add(new Vector3(109.5788f, 1414.899f, 29.33367f));
            BegGYToLeftMid.Add(new Vector3(112.5705f, 1412.096f, 28.77857f));
            BegGYToLeftMid.Add(new Vector3(115.5255f, 1409.328f, 29.67823f));
            BegGYToLeftMid.Add(new Vector3(118.4559f, 1406.584f, 30.31588f));
            BegGYToLeftMid.Add(new Vector3(121.3741f, 1403.85f, 30.7631f));
            BegGYToLeftMid.Add(new Vector3(124.2677f, 1401.14f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(127.1981f, 1398.395f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(130.1408f, 1395.638f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(133.2674f, 1392.71f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(136.3842f, 1390.142f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(139.3f, 1388.31f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(142.6064f, 1386.344f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(146.0359f, 1384.565f, 30.85853f));
            BegGYToLeftMid.Add(new Vector3(149.421f, 1383f, 30.91783f));
            BegGYToLeftMid.Add(new Vector3(152.8619f, 1381.435f, 30.93543f));
            BegGYToLeftMid.Add(new Vector3(156.1805f, 1379.926f, 31.19937f));
            BegGYToLeftMid.Add(new Vector3(160.6092f, 1377.938f, 31.40652f));
            BegGYToLeftMid.Add(new Vector3(164.3066f, 1377.421f, 31.68787f));
            BegGYToLeftMid.Add(new Vector3(168.2115f, 1377.149f, 31.8614f));
            BegGYToLeftMid.Add(new Vector3(172.4517f, 1376.854f, 32.00526f));
            BegGYToLeftMid.Add(new Vector3(176.2483f, 1377.089f, 31.92941f));
            BegGYToLeftMid.Add(new Vector3(179.7456f, 1377.553f, 32.24815f));
            BegGYToLeftMid.Add(new Vector3(183.791f, 1378.102f, 32.33066f));
            BegGYToLeftMid.Add(new Vector3(184.6733f, 1378.221f, 32.33382f), WaypointAction.Stop);

            LeftMidToRightMid.Add(new Vector3(180.8692f, 1367.079f, 32.54847f));
            LeftMidToRightMid.Add(new Vector3(179.4208f, 1368.216f, 32.55134f));
            LeftMidToRightMid.Add(new Vector3(177.3354f, 1366.255f, 32.53053f));
            LeftMidToRightMid.Add(new Vector3(174.7627f, 1362.979f, 32.31729f));
            LeftMidToRightMid.Add(new Vector3(172.3504f, 1360.72f, 32.09218f));
            LeftMidToRightMid.Add(new Vector3(169.3503f, 1358.231f, 31.84866f));
            LeftMidToRightMid.Add(new Vector3(166.6348f, 1355.979f, 31.60534f));
            LeftMidToRightMid.Add(new Vector3(163.9931f, 1353.796f, 31.06478f));
            LeftMidToRightMid.Add(new Vector3(160.7964f, 1351.508f, 30.93497f));
            LeftMidToRightMid.Add(new Vector3(157.528f, 1349.205f, 30.91788f));
            LeftMidToRightMid.Add(new Vector3(154.2734f, 1346.911f, 30.88351f));
            LeftMidToRightMid.Add(new Vector3(151.6177f, 1345.082f, 30.88351f));
            LeftMidToRightMid.Add(new Vector3(148.5008f, 1343.257f, 30.89034f));
            LeftMidToRightMid.Add(new Vector3(145.1185f, 1341.286f, 30.89208f));
            LeftMidToRightMid.Add(new Vector3(141.4769f, 1339.229f, 30.89208f));
            LeftMidToRightMid.Add(new Vector3(138.1396f, 1337.49f, 30.89208f));
            LeftMidToRightMid.Add(new Vector3(134.8917f, 1335.797f, 30.89208f));
            LeftMidToRightMid.Add(new Vector3(131.2713f, 1333.911f, 30.89208f));
            LeftMidToRightMid.Add(new Vector3(127.3678f, 1331.877f, 30.89907f));
            LeftMidToRightMid.Add(new Vector3(123.8666f, 1330.053f, 30.90926f));
            LeftMidToRightMid.Add(new Vector3(120.3797f, 1328.313f, 30.91163f));
            LeftMidToRightMid.Add(new Vector3(116.9095f, 1326.691f, 30.98397f));
            LeftMidToRightMid.Add(new Vector3(113.6084f, 1325.184f, 30.99132f));
            LeftMidToRightMid.Add(new Vector3(109.6602f, 1323.396f, 30.99278f));
            LeftMidToRightMid.Add(new Vector3(106.2658f, 1321.934f, 31.0316f));
            LeftMidToRightMid.Add(new Vector3(102.5608f, 1320.476f, 30.95398f));
            LeftMidToRightMid.Add(new Vector3(98.97935f, 1319.118f, 30.90668f));
            LeftMidToRightMid.Add(new Vector3(95.02095f, 1317.616f, 30.89346f));
            LeftMidToRightMid.Add(new Vector3(91.8287f, 1316.465f, 30.89346f));
            LeftMidToRightMid.Add(new Vector3(87.94057f, 1315.17f, 30.89346f));
            LeftMidToRightMid.Add(new Vector3(84.31416f, 1314.374f, 30.91244f));
            LeftMidToRightMid.Add(new Vector3(80.04518f, 1313.449f, 31.23302f));
            LeftMidToRightMid.Add(new Vector3(76.28519f, 1312.635f, 31.37135f));
            LeftMidToRightMid.Add(new Vector3(72.14755f, 1311.738f, 31.16594f));
            LeftMidToRightMid.Add(new Vector3(68.10843f, 1310.863f, 30.96029f));
            LeftMidToRightMid.Add(new Vector3(64.37711f, 1310.283f, 30.89654f));
            LeftMidToRightMid.Add(new Vector3(60.77074f, 1309.88f, 30.89342f));
            LeftMidToRightMid.Add(new Vector3(57.4612f, 1309.342f, 30.89901f));
            LeftMidToRightMid.Add(new Vector3(54.10054f, 1307.977f, 30.8936f));
            LeftMidToRightMid.Add(new Vector3(50.51624f, 1306.245f, 30.8936f));
            LeftMidToRightMid.Add(new Vector3(47.48275f, 1304.58f, 30.8936f));
            LeftMidToRightMid.Add(new Vector3(44.11438f, 1302.652f, 30.8936f));
            LeftMidToRightMid.Add(new Vector3(41.02508f, 1300.951f, 30.8921f));
            LeftMidToRightMid.Add(new Vector3(37.20451f, 1299.426f, 30.85306f));
            LeftMidToRightMid.Add(new Vector3(33.48769f, 1298.447f, 30.83759f));
            LeftMidToRightMid.Add(new Vector3(29.48309f, 1297.855f, 30.87026f));
            LeftMidToRightMid.Add(new Vector3(26.07244f, 1297.519f, 30.88328f));
            LeftMidToRightMid.Add(new Vector3(21.67418f, 1297.097f, 30.86742f));
            LeftMidToRightMid.Add(new Vector3(17.39298f, 1296.687f, 30.8893f));
            LeftMidToRightMid.Add(new Vector3(12.94456f, 1296.261f, 30.89263f));
            LeftMidToRightMid.Add(new Vector3(8.696806f, 1295.854f, 30.89263f));
            LeftMidToRightMid.Add(new Vector3(4.515951f, 1295.453f, 30.89263f));
            LeftMidToRightMid.Add(new Vector3(0.8702469f, 1295.104f, 30.89263f));
            LeftMidToRightMid.Add(new Vector3(-3.260441f, 1294.708f, 30.89263f));
            LeftMidToRightMid.Add(new Vector3(-6.989759f, 1294.351f, 30.89423f));
            LeftMidToRightMid.Add(new Vector3(-11.25424f, 1293.942f, 30.93112f));
            LeftMidToRightMid.Add(new Vector3(-15.20096f, 1293.564f, 30.93749f));
            LeftMidToRightMid.Add(new Vector3(-19.73301f, 1293.13f, 31.03613f));
            LeftMidToRightMid.Add(new Vector3(-24.11455f, 1292.71f, 30.99257f));
            LeftMidToRightMid.Add(new Vector3(-27.8013f, 1292.49f, 30.90037f));
            LeftMidToRightMid.Add(new Vector3(-32.14093f, 1292.501f, 30.9427f));
            LeftMidToRightMid.Add(new Vector3(-36.15612f, 1292.512f, 31.69702f));
            LeftMidToRightMid.Add(new Vector3(-39.6001f, 1292.521f, 32.12531f));
            LeftMidToRightMid.Add(new Vector3(-44.08569f, 1292.533f, 32.68314f));
            LeftMidToRightMid.Add(new Vector3(-48.08407f, 1292.544f, 33.18037f));
            LeftMidToRightMid.Add(new Vector3(-52.21686f, 1292.555f, 33.63606f));
            LeftMidToRightMid.Add(new Vector3(-56.13124f, 1292.565f, 33.77163f));
            LeftMidToRightMid.Add(new Vector3(-60.24723f, 1292.576f, 33.91424f));
            LeftMidToRightMid.Add(new Vector3(-64.07761f, 1292.586f, 34.04701f));
            LeftMidToRightMid.Add(new Vector3(-68.52891f, 1292.639f, 33.91695f));
            LeftMidToRightMid.Add(new Vector3(-72.93929f, 1292.902f, 33.76351f));
            LeftMidToRightMid.Add(new Vector3(-77.09301f, 1293.226f, 33.61955f));
            LeftMidToRightMid.Add(new Vector3(-81.03767f, 1293.625f, 33.14455f));
            LeftMidToRightMid.Add(new Vector3(-84.77817f, 1294.036f, 32.67934f));
            LeftMidToRightMid.Add(new Vector3(-89.07244f, 1294.625f, 32.14547f));
            LeftMidToRightMid.Add(new Vector3(-92.98389f, 1295.161f, 31.65909f));
            LeftMidToRightMid.Add(new Vector3(-97.22462f, 1295.768f, 30.97525f));
            LeftMidToRightMid.Add(new Vector3(-101.562f, 1296.513f, 30.8937f));
            LeftMidToRightMid.Add(new Vector3(-105.1894f, 1297.222f, 30.8937f));
            LeftMidToRightMid.Add(new Vector3(-109.2603f, 1298.026f, 30.89529f));
            LeftMidToRightMid.Add(new Vector3(-113.0012f, 1298.767f, 30.89529f));
            LeftMidToRightMid.Add(new Vector3(-117.2915f, 1299.888f, 30.89529f));
            LeftMidToRightMid.Add(new Vector3(-121.3514f, 1301.028f, 30.9696f));
            LeftMidToRightMid.Add(new Vector3(-125.7187f, 1302.253f, 31.05061f));
            LeftMidToRightMid.Add(new Vector3(-129.4227f, 1303.293f, 31.0008f));
            LeftMidToRightMid.Add(new Vector3(-133.5845f, 1304.502f, 30.84093f));
            LeftMidToRightMid.Add(new Vector3(-137.195f, 1305.73f, 30.84785f));
            LeftMidToRightMid.Add(new Vector3(-141.3134f, 1307.134f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-144.7322f, 1308.3f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-148.1032f, 1309.449f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-152.5237f, 1310.956f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-156.2615f, 1312.419f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-160.2859f, 1314.117f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-164.0782f, 1315.717f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-168.0717f, 1317.402f, 30.89244f));
            LeftMidToRightMid.Add(new Vector3(-171.7865f, 1318.97f, 30.88889f));
            LeftMidToRightMid.Add(new Vector3(-175.6561f, 1320.602f, 30.89099f));
            LeftMidToRightMid.Add(new Vector3(-179.3865f, 1322.176f, 30.89154f));
            LeftMidToRightMid.Add(new Vector3(-183.4419f, 1323.887f, 30.89157f));
            LeftMidToRightMid.Add(new Vector3(-186.6113f, 1325.641f, 30.89157f));
            LeftMidToRightMid.Add(new Vector3(-189.9924f, 1327.744f, 30.89157f));
            LeftMidToRightMid.Add(new Vector3(-193.6446f, 1330.015f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-196.7546f, 1331.949f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-200.3355f, 1334.176f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-203.845f, 1336.359f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-207.1976f, 1338.444f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-210.7927f, 1340.679f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-214.3878f, 1342.915f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-217.8688f, 1345.08f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-224.8022f, 1349.392f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-228.2261f, 1351.521f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-231.8783f, 1353.792f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-235.3878f, 1355.975f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-238.8545f, 1358.13f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-242.1613f, 1360.372f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-245.6102f, 1362.997f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-248.9924f, 1365.572f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-252.3344f, 1368.116f, 30.89162f));
            LeftMidToRightMid.Add(new Vector3(-255.6898f, 1370.67f, 31.06281f));
            LeftMidToRightMid.Add(new Vector3(-258.965f, 1373.162f, 31.45175f));
            LeftMidToRightMid.Add(new Vector3(-262.2f, 1375.625f, 31.56934f));
            LeftMidToRightMid.Add(new Vector3(-265.3549f, 1378.026f, 32.29988f));
            LeftMidToRightMid.Add(new Vector3(-268.5499f, 1380.458f, 32.76729f));
            LeftMidToRightMid.Add(new Vector3(-271.7448f, 1382.89f, 33.00233f));
            LeftMidToRightMid.Add(new Vector3(-275.0334f, 1385.393f, 32.8444f));
            LeftMidToRightMid.Add(new Vector3(-278.0412f, 1387.682f, 32.33839f));
            LeftMidToRightMid.Add(new Vector3(-281.1693f, 1390.063f, 32.08474f));
            LeftMidToRightMid.Add(new Vector3(-284.1504f, 1392.333f, 32.10883f));
            LeftMidToRightMid.Add(new Vector3(-286.6235f, 1394.215f, 32.08158f), WaypointAction.Stop);

            RightMidToUpper.Add(new Vector3(-285.0174f, 1393.318f, 32.12558f));
            RightMidToUpper.Add(new Vector3(-281.6143f, 1391.418f, 32.10354f));
            RightMidToUpper.Add(new Vector3(-278.1086f, 1389.461f, 32.25365f));
            RightMidToUpper.Add(new Vector3(-274.6028f, 1387.503f, 32.72f));
            RightMidToUpper.Add(new Vector3(-271.3171f, 1385.669f, 32.89127f));
            RightMidToUpper.Add(new Vector3(-267.4446f, 1383.506f, 32.57367f));
            RightMidToUpper.Add(new Vector3(-264.1149f, 1381.647f, 32.1039f));
            RightMidToUpper.Add(new Vector3(-260.8194f, 1379.73f, 31.80215f));
            RightMidToUpper.Add(new Vector3(-257.8827f, 1377.867f, 31.65935f));
            RightMidToUpper.Add(new Vector3(-254.0168f, 1375.399f, 31.15213f));
            RightMidToUpper.Add(new Vector3(-250.3774f, 1373.076f, 30.89562f));
            RightMidToUpper.Add(new Vector3(-246.9222f, 1370.871f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-243.0563f, 1368.403f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-239.5727f, 1366.179f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-236.5423f, 1364.245f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-233.1393f, 1362.051f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-230.1914f, 1359.908f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-227.1614f, 1357.456f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-223.3303f, 1354.105f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-220.47f, 1351.384f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-217.4879f, 1348.475f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-214.3271f, 1344.991f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-211.1624f, 1341.354f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-208.3419f, 1338.063f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-205.5278f, 1334.484f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-203.0873f, 1331.046f, 30.89405f));
            RightMidToUpper.Add(new Vector3(-201.0037f, 1327.872f, 30.91138f));
            RightMidToUpper.Add(new Vector3(-198.8139f, 1324.426f, 31.00724f));
            RightMidToUpper.Add(new Vector3(-196.8911f, 1321.27f, 31.19222f));
            RightMidToUpper.Add(new Vector3(-194.9869f, 1318.141f, 31.33958f));
            RightMidToUpper.Add(new Vector3(-193.2363f, 1314.584f, 31.48236f));
            RightMidToUpper.Add(new Vector3(-191.8495f, 1311.374f, 31.64176f));
            RightMidToUpper.Add(new Vector3(-190.7292f, 1307.922f, 31.76303f));
            RightMidToUpper.Add(new Vector3(-189.6003f, 1303.724f, 31.62949f));
            RightMidToUpper.Add(new Vector3(-188.7023f, 1299.552f, 31.87589f));
            RightMidToUpper.Add(new Vector3(-188.1941f, 1296.164f, 32.48678f));
            RightMidToUpper.Add(new Vector3(-188.0185f, 1291.924f, 33.44753f));
            RightMidToUpper.Add(new Vector3(-188.2349f, 1287.781f, 34.42542f));
            RightMidToUpper.Add(new Vector3(-189.0105f, 1282.964f, 35.80592f));
            RightMidToUpper.Add(new Vector3(-189.8995f, 1279.049f, 37.20222f));
            RightMidToUpper.Add(new Vector3(-191.2128f, 1274.768f, 38.92683f));
            RightMidToUpper.Add(new Vector3(-192.7779f, 1270.763f, 40.65219f));
            RightMidToUpper.Add(new Vector3(-194.6224f, 1266.271f, 42.5887f));
            RightMidToUpper.Add(new Vector3(-196.2291f, 1262.464f, 44.21701f));
            RightMidToUpper.Add(new Vector3(-197.8226f, 1258.687f, 45.82813f));
            RightMidToUpper.Add(new Vector3(-199.475f, 1254.771f, 47.4707f));
            RightMidToUpper.Add(new Vector3(-201.16f, 1250.777f, 49.10155f));
            RightMidToUpper.Add(new Vector3(-202.6817f, 1247.171f, 50.48166f));
            RightMidToUpper.Add(new Vector3(-204.1185f, 1243.766f, 51.70724f));
            RightMidToUpper.Add(new Vector3(-205.5296f, 1240.259f, 52.8428f));
            RightMidToUpper.Add(new Vector3(-206.8796f, 1236.836f, 53.84091f));
            RightMidToUpper.Add(new Vector3(-208.49f, 1232.885f, 54.74749f));
            RightMidToUpper.Add(new Vector3(-210.1907f, 1228.898f, 55.97915f));
            RightMidToUpper.Add(new Vector3(-211.687f, 1225.391f, 55.43687f));
            RightMidToUpper.Add(new Vector3(-213.0581f, 1222.176f, 55.47705f));
            RightMidToUpper.Add(new Vector3(-214.5083f, 1218.777f, 55.51853f));
            RightMidToUpper.Add(new Vector3(-216.1694f, 1214.883f, 55.7183f));
            RightMidToUpper.Add(new Vector3(-217.8767f, 1210.88f, 56.0882f));
            RightMidToUpper.Add(new Vector3(-219.5444f, 1206.971f, 56.56151f));
            RightMidToUpper.Add(new Vector3(-221.1989f, 1203.092f, 57.06503f));
            RightMidToUpper.Add(new Vector3(-222.6557f, 1199.677f, 57.55376f));
            RightMidToUpper.Add(new Vector3(-223.8287f, 1195.667f, 58.24727f));
            RightMidToUpper.Add(new Vector3(-224.3093f, 1192.19f, 58.87698f));
            RightMidToUpper.Add(new Vector3(-224.1064f, 1188.212f, 59.55348f));
            RightMidToUpper.Add(new Vector3(-223.0878f, 1184.036f, 60.34f));
            RightMidToUpper.Add(new Vector3(-221.7704f, 1179.713f, 61.28057f));
            RightMidToUpper.Add(new Vector3(-220.4673f, 1175.99f, 62.17237f));
            RightMidToUpper.Add(new Vector3(-218.7742f, 1172.537f, 63.04491f));
            RightMidToUpper.Add(new Vector3(-216.5567f, 1169.44f, 63.76207f));
            RightMidToUpper.Add(new Vector3(-213.9525f, 1166.081f, 64.50336f));
            RightMidToUpper.Add(new Vector3(-211.1918f, 1162.589f, 65.19328f));
            RightMidToUpper.Add(new Vector3(-208.9312f, 1159.728f, 65.6692f));
            RightMidToUpper.Add(new Vector3(-206.1565f, 1156.977f, 66.12053f));
            RightMidToUpper.Add(new Vector3(-203.4471f, 1155.072f, 66.44534f));
            RightMidToUpper.Add(new Vector3(-199.9371f, 1152.616f, 66.73205f));
            RightMidToUpper.Add(new Vector3(-196.9128f, 1150.828f, 66.86343f));
            RightMidToUpper.Add(new Vector3(-193.3282f, 1148.982f, 67.00793f));
            RightMidToUpper.Add(new Vector3(-190.2563f, 1147.466f, 67.12875f));
            RightMidToUpper.Add(new Vector3(-186.0351f, 1147.348f, 67.3857f));
            RightMidToUpper.Add(new Vector3(-181.92f, 1147.434f, 67.6983f));
            RightMidToUpper.Add(new Vector3(-177.2668f, 1147.494f, 67.99142f));
            RightMidToUpper.Add(new Vector3(-173.7893f, 1147.496f, 68.19222f));
            RightMidToUpper.Add(new Vector3(-169.4885f, 1147.485f, 68.48878f));
            RightMidToUpper.Add(new Vector3(-166.0445f, 1147.476f, 68.70168f));
            RightMidToUpper.Add(new Vector3(-161.8445f, 1147.464f, 68.97176f));
            RightMidToUpper.Add(new Vector3(-158.1317f, 1147.455f, 69.32716f));
            RightMidToUpper.Add(new Vector3(-154.2173f, 1147.444f, 69.09931f));
            RightMidToUpper.Add(new Vector3(-150.1013f, 1147.433f, 69.00597f));
            RightMidToUpper.Add(new Vector3(-146.4395f, 1147.462f, 69.33004f));
            RightMidToUpper.Add(new Vector3(-142.4843f, 1147.734f, 69.46643f));
            RightMidToUpper.Add(new Vector3(-138.9671f, 1148.28f, 69.4166f));
            RightMidToUpper.Add(new Vector3(-135.3422f, 1149.151f, 69.42947f));
            RightMidToUpper.Add(new Vector3(-131.0233f, 1150.482f, 69.53711f));
            RightMidToUpper.Add(new Vector3(-127.5201f, 1151.619f, 69.75186f));
            RightMidToUpper.Add(new Vector3(-123.8706f, 1153.078f, 69.88259f));
            RightMidToUpper.Add(new Vector3(-119.7923f, 1155.062f, 69.58555f));
            RightMidToUpper.Add(new Vector3(-116.3276f, 1156.989f, 69.42132f));
            RightMidToUpper.Add(new Vector3(-113.1348f, 1158.818f, 69.3331f));
            RightMidToUpper.Add(new Vector3(-109.6022f, 1160.929f, 69.31756f));
            RightMidToUpper.Add(new Vector3(-105.9805f, 1163.218f, 69.29243f));
            RightMidToUpper.Add(new Vector3(-103.0541f, 1165.36f, 69.19576f));
            RightMidToUpper.Add(new Vector3(-100.3037f, 1167.752f, 69.05412f));
            RightMidToUpper.Add(new Vector3(-97.75464f, 1170.262f, 69.07439f));
            RightMidToUpper.Add(new Vector3(-95.42455f, 1173.302f, 69.19372f));
            RightMidToUpper.Add(new Vector3(-93.22794f, 1176.212f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-91.3622f, 1179.099f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-89.82515f, 1182.349f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-88.05669f, 1186.158f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-86.47215f, 1189.572f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-84.61172f, 1193.579f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-82.7873f, 1197.455f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-80.70472f, 1200.526f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-78.44783f, 1203.147f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-75.65309f, 1205.345f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-72.33333f, 1206.77f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-68.31512f, 1206.321f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-65.70604f, 1204.224f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-64.30601f, 1201.067f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-61.17185f, 1192.805f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-60.17678f, 1190.182f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-58.65142f, 1186.16f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-57.29289f, 1182.579f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-55.77944f, 1178.589f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-54.56391f, 1175.385f, 70.08427f));
            RightMidToUpper.Add(new Vector3(-52.53199f, 1172.434f, 69.09448f));
            RightMidToUpper.Add(new Vector3(-50.16342f, 1169.339f, 68.9706f));
            RightMidToUpper.Add(new Vector3(-47.74381f, 1166.177f, 68.83768f));
            RightMidToUpper.Add(new Vector3(-45.44505f, 1163.198f, 68.77528f));
            RightMidToUpper.Add(new Vector3(-42.65448f, 1160.674f, 68.70879f));
            RightMidToUpper.Add(new Vector3(-39.50982f, 1157.864f, 68.69173f));
            RightMidToUpper.Add(new Vector3(-36.49617f, 1155.172f, 68.72526f));
            RightMidToUpper.Add(new Vector3(-33.52691f, 1152.519f, 68.79223f));
            RightMidToUpper.Add(new Vector3(-30.54511f, 1149.856f, 68.89555f));
            RightMidToUpper.Add(new Vector3(-27.87653f, 1147.472f, 68.96228f));
            RightMidToUpper.Add(new Vector3(-24.53141f, 1144.483f, 69.03778f));
            RightMidToUpper.Add(new Vector3(-21.24893f, 1141.551f, 69.08968f));
            RightMidToUpper.Add(new Vector3(-18.29219f, 1138.909f, 69.1264f));
            RightMidToUpper.Add(new Vector3(-15.41063f, 1136.335f, 69.14751f));
            RightMidToUpper.Add(new Vector3(-12.07804f, 1133.358f, 69.13169f));
            RightMidToUpper.Add(new Vector3(-8.996012f, 1130.604f, 69.09196f));
            RightMidToUpper.Add(new Vector3(-6.152033f, 1128.063f, 69.06599f));
            RightMidToUpper.Add(new Vector3(-4.007555f, 1125.247f, 69.06062f));
            RightMidToUpper.Add(new Vector3(-2.61171f, 1121.916f, 69.12025f));
            RightMidToUpper.Add(new Vector3(-1.23164f, 1118.596f, 69.17695f));
            RightMidToUpper.Add(new Vector3(0.4963759f, 1114.439f, 69.26494f), WaypointAction.Stop);

            DefenseStartToUpper.Add(new Vector3(-61.64254f, 1234.219f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-60.87322f, 1231.086f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-60.46411f, 1227.489f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-60.20341f, 1223.162f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-59.94878f, 1218.936f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-59.7345f, 1215.381f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-59.42396f, 1210.906f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-59.63898f, 1206.49f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-60.5868f, 1201.952f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-61.46734f, 1197.914f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-62.60523f, 1194.245f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-64.29546f, 1190.604f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-66.09219f, 1187.183f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-68.27274f, 1183.168f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-70.23685f, 1179.551f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-72.07148f, 1175.624f, 70.08422f));
            DefenseStartToUpper.Add(new Vector3(-71.92476f, 1171.993f, 69.21471f));
            DefenseStartToUpper.Add(new Vector3(-71.58345f, 1168.43f, 69.20148f));
            DefenseStartToUpper.Add(new Vector3(-71.32381f, 1164.379f, 69.24324f));
            DefenseStartToUpper.Add(new Vector3(-71.5778f, 1160.726f, 69.39774f));
            DefenseStartToUpper.Add(new Vector3(-71.7421f, 1156.563f, 69.66766f));
            DefenseStartToUpper.Add(new Vector3(-71.90056f, 1151.307f, 69.92635f));
            DefenseStartToUpper.Add(new Vector3(-72.00282f, 1147.915f, 70.1188f));
            DefenseStartToUpper.Add(new Vector3(-70.68655f, 1144.907f, 70.36703f));
            DefenseStartToUpper.Add(new Vector3(-67.88565f, 1142.427f, 70.57381f));
            DefenseStartToUpper.Add(new Vector3(-64.81335f, 1140.487f, 70.65897f));
            DefenseStartToUpper.Add(new Vector3(-61.0475f, 1138.955f, 70.63486f));
            DefenseStartToUpper.Add(new Vector3(-57.62006f, 1137.572f, 70.34609f));
            DefenseStartToUpper.Add(new Vector3(-54.145f, 1136.668f, 69.69495f));
            DefenseStartToUpper.Add(new Vector3(-45.61574f, 1134.702f, 69.04787f));
            DefenseStartToUpper.Add(new Vector3(-41.39203f, 1133.729f, 68.8332f));
            DefenseStartToUpper.Add(new Vector3(-37.2993f, 1132.786f, 68.76055f));
            DefenseStartToUpper.Add(new Vector3(-33.15745f, 1131.832f, 68.81366f));
            DefenseStartToUpper.Add(new Vector3(-29.27753f, 1130.937f, 68.93103f));
            DefenseStartToUpper.Add(new Vector3(-25.44005f, 1129.883f, 69.06609f));
            DefenseStartToUpper.Add(new Vector3(-21.19528f, 1128.435f, 69.20509f));
            DefenseStartToUpper.Add(new Vector3(-17.58134f, 1126.547f, 69.31388f));
            DefenseStartToUpper.Add(new Vector3(-14.27375f, 1124.649f, 69.39153f));
            DefenseStartToUpper.Add(new Vector3(-11.07227f, 1121.982f, 69.48465f));
            DefenseStartToUpper.Add(new Vector3(-8.297329f, 1119.342f, 69.61351f));
            DefenseStartToUpper.Add(new Vector3(-5.352f, 1116.539f, 69.55003f));
            DefenseStartToUpper.Add(new Vector3(-2.637917f, 1113.957f, 69.42141f), WaypointAction.Stop);

            DefenseUpper.Add(new Vector3(-3.543752f, 1115.559f, 69.42068f));
            DefenseUpper.Add(new Vector3(-5.194742f, 1118.127f, 69.43752f));
            DefenseUpper.Add(new Vector3(-4.432282f, 1121.774f, 69.20422f));
            DefenseUpper.Add(new Vector3(-1.748475f, 1124.998f, 69.00066f));
            DefenseUpper.Add(new Vector3(2.359859f, 1127.513f, 68.9356f));
            DefenseUpper.Add(new Vector3(6.862881f, 1129.368f, 68.97612f));
            DefenseUpper.Add(new Vector3(10.65594f, 1130.573f, 69.0613f));
            DefenseUpper.Add(new Vector3(14.83723f, 1131.715f, 69.08173f));
            DefenseUpper.Add(new Vector3(19.1021f, 1132.87f, 68.96913f));
            DefenseUpper.Add(new Vector3(22.91292f, 1133.901f, 68.95448f));
            DefenseUpper.Add(new Vector3(27.84266f, 1135.236f, 69.04498f));
            DefenseUpper.Add(new Vector3(32.05888f, 1136.378f, 69.07308f));
            DefenseUpper.Add(new Vector3(36.16159f, 1137.488f, 68.99998f));
            DefenseUpper.Add(new Vector3(40.37251f, 1138.648f, 68.78529f));
            DefenseUpper.Add(new Vector3(44.01553f, 1140.664f, 68.51549f));
            DefenseUpper.Add(new Vector3(46.92492f, 1142.916f, 68.22455f));
            DefenseUpper.Add(new Vector3(50.29332f, 1145.536f, 67.74044f));
            DefenseUpper.Add(new Vector3(53.6352f, 1148.135f, 67.47414f));
            DefenseUpper.Add(new Vector3(59.47022f, 1152.673f, 66.87505f));
            DefenseUpper.Add(new Vector3(62.82536f, 1155.283f, 66.07314f));
            DefenseUpper.Add(new Vector3(66.19743f, 1158.102f, 64.76131f));
            DefenseUpper.Add(new Vector3(68.6179f, 1161.572f, 63.41622f));
            DefenseUpper.Add(new Vector3(70.02599f, 1164.898f, 62.71024f));
            DefenseUpper.Add(new Vector3(71.33157f, 1168.551f, 62.18219f));
            DefenseUpper.Add(new Vector3(72.54063f, 1172.521f, 60.79746f));
            DefenseUpper.Add(new Vector3(73.70612f, 1176.381f, 59.61615f));
            DefenseUpper.Add(new Vector3(74.95901f, 1180.53f, 58.3529f));
            DefenseUpper.Add(new Vector3(76.10993f, 1184.342f, 57.6884f));
            DefenseUpper.Add(new Vector3(77.14916f, 1187.784f, 57.15136f));
            DefenseUpper.Add(new Vector3(78.31054f, 1191.184f, 56.42268f));
            DefenseUpper.Add(new Vector3(80.12276f, 1194.451f, 55.7715f));
            DefenseUpper.Add(new Vector3(83.30547f, 1196.499f, 55.34778f));
            DefenseUpper.Add(new Vector3(87.02129f, 1195.824f, 55.2736f));
            DefenseUpper.Add(new Vector3(89.85524f, 1193.935f, 55.45103f));
            DefenseUpper.Add(new Vector3(91.81499f, 1190.486f, 55.89364f));
            DefenseUpper.Add(new Vector3(92.61903f, 1186.765f, 56.42238f));
            DefenseUpper.Add(new Vector3(92.81359f, 1182.951f, 57.01595f));
            DefenseUpper.Add(new Vector3(92.61458f, 1179.513f, 57.58625f));
            DefenseUpper.Add(new Vector3(91.80125f, 1175.856f, 58.19628f));
            DefenseUpper.Add(new Vector3(90.07869f, 1172.665f, 58.78709f));
            DefenseUpper.Add(new Vector3(87.54253f, 1170f, 59.35372f));
            DefenseUpper.Add(new Vector3(85.09901f, 1167.432f, 59.97564f));
            DefenseUpper.Add(new Vector3(82.46772f, 1164.272f, 60.76815f));
            DefenseUpper.Add(new Vector3(79.49684f, 1160.432f, 61.712f));
            DefenseUpper.Add(new Vector3(77.225f, 1157.495f, 62.38932f));
            DefenseUpper.Add(new Vector3(75.00455f, 1154.625f, 62.95066f));
            DefenseUpper.Add(new Vector3(71.81187f, 1151.362f, 63.59967f));
            DefenseUpper.Add(new Vector3(68.82824f, 1149.244f, 64.33511f));
            DefenseUpper.Add(new Vector3(65.53555f, 1147.353f, 65.05689f));
            DefenseUpper.Add(new Vector3(61.55458f, 1145.076f, 65.65842f));
            DefenseUpper.Add(new Vector3(58.20064f, 1143.157f, 66.31796f));
            DefenseUpper.Add(new Vector3(54.58422f, 1141.088f, 66.91848f));
            DefenseUpper.Add(new Vector3(50.64699f, 1138.836f, 67.48492f));
            DefenseUpper.Add(new Vector3(47.59928f, 1137.092f, 67.86407f));
            DefenseUpper.Add(new Vector3(43.90366f, 1135.972f, 68.26044f));
            DefenseUpper.Add(new Vector3(40.52319f, 1135.156f, 68.53761f));
            DefenseUpper.Add(new Vector3(36.76711f, 1134.249f, 68.72448f));
            DefenseUpper.Add(new Vector3(33.10901f, 1133.366f, 68.77991f));
            DefenseUpper.Add(new Vector3(29.28283f, 1132.818f, 68.80961f));
            DefenseUpper.Add(new Vector3(25.99179f, 1132.71f, 68.83035f));
            DefenseUpper.Add(new Vector3(22.24739f, 1132.588f, 68.85968f));
            DefenseUpper.Add(new Vector3(18.36576f, 1132.461f, 68.97482f));
            DefenseUpper.Add(new Vector3(14.7389f, 1132.342f, 69.11982f));
            DefenseUpper.Add(new Vector3(6.763168f, 1132.081f, 69.13728f));
            DefenseUpper.Add(new Vector3(2.985189f, 1131.958f, 69.07489f));
            DefenseUpper.Add(new Vector3(-1.111818f, 1131.824f, 69.04229f));
            DefenseUpper.Add(new Vector3(-5.292784f, 1131.687f, 69.04993f));
            DefenseUpper.Add(new Vector3(-9.272255f, 1131.557f, 69.09444f));
            DefenseUpper.Add(new Vector3(-13.55396f, 1131.417f, 69.15879f));
            DefenseUpper.Add(new Vector3(-18.08754f, 1131.269f, 69.13666f));
            DefenseUpper.Add(new Vector3(-22.11738f, 1131.137f, 69.08135f));
            DefenseUpper.Add(new Vector3(-26.33193f, 1130.999f, 68.99766f));
            DefenseUpper.Add(new Vector3(-30.26103f, 1130.87f, 68.90673f));
            DefenseUpper.Add(new Vector3(-34.32446f, 1130.737f, 68.83691f));
            DefenseUpper.Add(new Vector3(-38.30392f, 1130.607f, 68.8379f));
            DefenseUpper.Add(new Vector3(-42.30019f, 1130.477f, 68.92836f));
            DefenseUpper.Add(new Vector3(-46.58191f, 1130.337f, 69.08196f));
            DefenseUpper.Add(new Vector3(-50.62853f, 1130.204f, 69.21017f));
            DefenseUpper.Add(new Vector3(-54.54085f, 1130.076f, 69.29962f));
            DefenseUpper.Add(new Vector3(-58.73861f, 1129.939f, 69.37737f));
            DefenseUpper.Add(new Vector3(-62.65142f, 1129.811f, 69.49018f));
            DefenseUpper.Add(new Vector3(-66.74842f, 1129.677f, 69.46902f));
            DefenseUpper.Add(new Vector3(-70.82864f, 1129.543f, 69.4962f));
            DefenseUpper.Add(new Vector3(-75.32864f, 1129.396f, 69.4385f));
            DefenseUpper.Add(new Vector3(-79.42564f, 1129.262f, 69.31709f));
            DefenseUpper.Add(new Vector3(-83.53944f, 1129.128f, 69.23158f));
            DefenseUpper.Add(new Vector3(-87.65324f, 1128.993f, 69.19688f));
            DefenseUpper.Add(new Vector3(-91.78383f, 1128.858f, 69.23102f));
            DefenseUpper.Add(new Vector3(-95.62897f, 1128.732f, 69.3764f));
            DefenseUpper.Add(new Vector3(-99.57487f, 1128.603f, 69.48753f));
            DefenseUpper.Add(new Vector3(-103.2689f, 1128.482f, 69.50552f));
            DefenseUpper.Add(new Vector3(-107.2652f, 1128.352f, 69.54679f));
            DefenseUpper.Add(new Vector3(-111.3118f, 1128.219f, 69.54652f));
            DefenseUpper.Add(new Vector3(-115.3248f, 1128.088f, 69.50385f));
            DefenseUpper.Add(new Vector3(-118.9349f, 1127.97f, 69.33507f));
            DefenseUpper.Add(new Vector3(-122.5282f, 1127.852f, 69.14423f));
            DefenseUpper.Add(new Vector3(-126.6084f, 1127.719f, 68.98594f));
            DefenseUpper.Add(new Vector3(-130.4032f, 1127.595f, 68.83545f));
            DefenseUpper.Add(new Vector3(-134.198f, 1127.471f, 68.80684f));
            DefenseUpper.Add(new Vector3(-138.1271f, 1127.343f, 68.92991f));
            DefenseUpper.Add(new Vector3(-141.8409f, 1127.639f, 68.99232f));
            DefenseUpper.Add(new Vector3(-145.5318f, 1128.041f, 68.99436f));
            DefenseUpper.Add(new Vector3(-149.0724f, 1128.427f, 68.96996f));
            DefenseUpper.Add(new Vector3(-152.9971f, 1128.856f, 68.97875f));
            DefenseUpper.Add(new Vector3(-156.6902f, 1129.446f, 69.00848f));
            DefenseUpper.Add(new Vector3(-160.1701f, 1130.35f, 68.95359f));
            DefenseUpper.Add(new Vector3(-163.9564f, 1131.846f, 68.72862f));
            DefenseUpper.Add(new Vector3(-167.2186f, 1133.546f, 68.44359f));
            DefenseUpper.Add(new Vector3(-170.4292f, 1135.273f, 68.22353f));
            DefenseUpper.Add(new Vector3(-173.9061f, 1137.144f, 68.00343f));
            DefenseUpper.Add(new Vector3(-177.2054f, 1138.918f, 67.83829f));
            DefenseUpper.Add(new Vector3(-180.9338f, 1140.924f, 67.63392f));
            DefenseUpper.Add(new Vector3(-184.4551f, 1142.818f, 67.42819f));
            DefenseUpper.Add(new Vector3(-187.7989f, 1144.617f, 67.24379f));
            DefenseUpper.Add(new Vector3(-191.187f, 1146.439f, 67.06631f));
            DefenseUpper.Add(new Vector3(-195.0486f, 1148.516f, 66.92467f));
            DefenseUpper.Add(new Vector3(-198.5107f, 1150.379f, 66.78279f));
            DefenseUpper.Add(new Vector3(-201.7565f, 1152.281f, 66.60794f));
            DefenseUpper.Add(new Vector3(-204.931f, 1154.566f, 66.32657f));
            DefenseUpper.Add(new Vector3(-207.5977f, 1156.925f, 65.97751f));
            DefenseUpper.Add(new Vector3(-210.1945f, 1159.646f, 65.53987f));
            DefenseUpper.Add(new Vector3(-212.6481f, 1162.759f, 65.01195f));
            DefenseUpper.Add(new Vector3(-214.9178f, 1166.049f, 64.40016f));
            DefenseUpper.Add(new Vector3(-216.7426f, 1169.356f, 63.75493f));
            DefenseUpper.Add(new Vector3(-217.8398f, 1172.685f, 63.10875f));
            DefenseUpper.Add(new Vector3(-218.5952f, 1176.508f, 62.24961f));
            DefenseUpper.Add(new Vector3(-218.7845f, 1180.662f, 61.27512f));
            DefenseUpper.Add(new Vector3(-218.4205f, 1184.624f, 60.36904f));
            DefenseUpper.Add(new Vector3(-217.7927f, 1188.249f, 59.55925f));
            DefenseUpper.Add(new Vector3(-216.9033f, 1192.298f, 59.00828f));
            DefenseUpper.Add(new Vector3(-215.7767f, 1196.03f, 58.4273f));
            DefenseUpper.Add(new Vector3(-214.6162f, 1199.873f, 57.61947f));
            DefenseUpper.Add(new Vector3(-214.6918f, 1203.053f, 56.95125f));
            DefenseUpper.Add(new Vector3(-217.4647f, 1206.278f, 56.55486f));
            DefenseUpper.Add(new Vector3(-220.8041f, 1208.637f, 56.44069f));
            DefenseUpper.Add(new Vector3(-224.6841f, 1208.704f, 56.64294f));
            DefenseUpper.Add(new Vector3(-228.4401f, 1207.018f, 57.05649f));
            DefenseUpper.Add(new Vector3(-229.9911f, 1203.714f, 57.50866f));
            DefenseUpper.Add(new Vector3(-229.8465f, 1199.903f, 57.98064f));
            DefenseUpper.Add(new Vector3(-228.5238f, 1195.688f, 58.49786f));
            DefenseUpper.Add(new Vector3(-227.3771f, 1192.175f, 58.98367f));
            DefenseUpper.Add(new Vector3(-226.0584f, 1188.134f, 59.60182f));
            DefenseUpper.Add(new Vector3(-224.7814f, 1184.221f, 60.28598f));
            DefenseUpper.Add(new Vector3(-223.4859f, 1180.531f, 61.04303f));
            DefenseUpper.Add(new Vector3(-221.5662f, 1177.178f, 61.84011f));
            DefenseUpper.Add(new Vector3(-219.6465f, 1173.883f, 62.69618f));
            DefenseUpper.Add(new Vector3(-217.1914f, 1170.798f, 63.51225f));
            DefenseUpper.Add(new Vector3(-214.7359f, 1167.903f, 64.17865f));
            DefenseUpper.Add(new Vector3(-212.0546f, 1165.534f, 64.77238f));
            DefenseUpper.Add(new Vector3(-209.3689f, 1163.247f, 65.32565f));
            DefenseUpper.Add(new Vector3(-206.203f, 1160.649f, 65.97779f));
            DefenseUpper.Add(new Vector3(-203.0293f, 1158.996f, 66.44919f));
            DefenseUpper.Add(new Vector3(-199.5085f, 1157.173f, 66.79242f));
            DefenseUpper.Add(new Vector3(-196.3756f, 1155.551f, 67.0899f));
            DefenseUpper.Add(new Vector3(-192.6362f, 1153.64f, 67.24351f));
            DefenseUpper.Add(new Vector3(-189.3917f, 1152.388f, 67.33672f));
            DefenseUpper.Add(new Vector3(-186.1781f, 1151.15f, 67.50005f));
            DefenseUpper.Add(new Vector3(-182.6823f, 1149.802f, 67.74538f));
            DefenseUpper.Add(new Vector3(-178.8573f, 1148.328f, 67.93804f));
            DefenseUpper.Add(new Vector3(-175.2204f, 1146.927f, 68.08025f));
            DefenseUpper.Add(new Vector3(-171.8971f, 1145.646f, 68.24606f));
            DefenseUpper.Add(new Vector3(-167.9624f, 1144.13f, 68.43594f));
            DefenseUpper.Add(new Vector3(-164.1531f, 1142.661f, 68.59361f));
            DefenseUpper.Add(new Vector3(-160.7411f, 1141.347f, 68.75485f));
            DefenseUpper.Add(new Vector3(-156.9318f, 1139.879f, 68.867f));
            DefenseUpper.Add(new Vector3(-153.4673f, 1138.543f, 68.84926f));
            DefenseUpper.Add(new Vector3(-149.7364f, 1137.105f, 68.81962f));
            DefenseUpper.Add(new Vector3(-145.8644f, 1135.613f, 68.84046f));
            DefenseUpper.Add(new Vector3(-142.4156f, 1134.284f, 68.85364f));
            DefenseUpper.Add(new Vector3(-138.8791f, 1132.951f, 68.92136f));
            DefenseUpper.Add(new Vector3(-135.0095f, 1132.09f, 68.99032f));
            DefenseUpper.Add(new Vector3(-131.5595f, 1131.353f, 69.01684f));
            DefenseUpper.Add(new Vector3(-127.9575f, 1131.127f, 69.11872f));
            DefenseUpper.Add(new Vector3(-124.0801f, 1130.965f, 69.30832f));
            DefenseUpper.Add(new Vector3(-120.3873f, 1130.81f, 69.58062f));
            DefenseUpper.Add(new Vector3(-116.9296f, 1130.665f, 69.7303f));
            DefenseUpper.Add(new Vector3(-113.3876f, 1130.551f, 69.83748f));
            DefenseUpper.Add(new Vector3(-109.5787f, 1130.816f, 69.91303f));
            DefenseUpper.Add(new Vector3(-105.5986f, 1131.198f, 69.94843f));
            DefenseUpper.Add(new Vector3(-101.886f, 1131.553f, 69.79424f));
            DefenseUpper.Add(new Vector3(-97.87236f, 1131.938f, 69.61109f));
            DefenseUpper.Add(new Vector3(-93.70824f, 1132.337f, 69.31989f));
            DefenseUpper.Add(new Vector3(-89.91203f, 1132.701f, 69.18988f));
            DefenseUpper.Add(new Vector3(-85.84825f, 1133.09f, 69.18833f));
            DefenseUpper.Add(new Vector3(-81.79737f, 1133.21f, 69.28445f));
            DefenseUpper.Add(new Vector3(-77.68172f, 1133.264f, 69.58886f));
            DefenseUpper.Add(new Vector3(-73.83485f, 1133.314f, 69.92599f));
            DefenseUpper.Add(new Vector3(-69.75279f, 1133.365f, 69.83382f));
            DefenseUpper.Add(new Vector3(-65.70671f, 1132.982f, 69.69995f));
            DefenseUpper.Add(new Vector3(-62.27098f, 1132.567f, 69.76881f));
            DefenseUpper.Add(new Vector3(-58.43498f, 1132.102f, 69.44388f));
            DefenseUpper.Add(new Vector3(-54.29876f, 1131.602f, 69.33496f));
            DefenseUpper.Add(new Vector3(-50.26345f, 1131.107f, 69.20856f));
            DefenseUpper.Add(new Vector3(-46.41891f, 1130.466f, 69.07539f));
            DefenseUpper.Add(new Vector3(-42.60903f, 1129.822f, 68.95473f));
            DefenseUpper.Add(new Vector3(-38.51756f, 1129.13f, 68.892f));
            DefenseUpper.Add(new Vector3(-35.13395f, 1128.409f, 68.93507f));
            DefenseUpper.Add(new Vector3(-31.6501f, 1127.592f, 69.03276f));
            DefenseUpper.Add(new Vector3(-28.03542f, 1126.744f, 69.15968f));
            DefenseUpper.Add(new Vector3(-24.15903f, 1125.835f, 69.26697f));
            DefenseUpper.Add(new Vector3(-19.81533f, 1124.816f, 69.3605f));
            DefenseUpper.Add(new Vector3(-15.72632f, 1123.857f, 69.43513f));

            DefenseTop.Add(new Vector3(-179.7921f, 965.9156f, 91.48891f));
            DefenseTop.Add(new Vector3(-177.4867f, 968.36f, 91.38622f));
            DefenseTop.Add(new Vector3(-174.8976f, 971.1365f, 91.27474f));
            DefenseTop.Add(new Vector3(-172.8684f, 974.636f, 91.05015f));
            DefenseTop.Add(new Vector3(-170.9751f, 978.0621f, 90.8399f));
            DefenseTop.Add(new Vector3(-168.8543f, 981.9f, 90.6111f));
            DefenseTop.Add(new Vector3(-167.0992f, 985.076f, 90.4396f));
            DefenseTop.Add(new Vector3(-165.1003f, 988.6933f, 90.26406f));
            DefenseTop.Add(new Vector3(-163.0608f, 992.3841f, 90.13511f));
            DefenseTop.Add(new Vector3(-161.0619f, 996.0013f, 90.26161f));
            DefenseTop.Add(new Vector3(-158.7705f, 1000.148f, 90.21313f));
            DefenseTop.Add(new Vector3(-156.0131f, 1004.879f, 89.88067f));
            DefenseTop.Add(new Vector3(-153.6654f, 1008.481f, 89.57153f));
            DefenseTop.Add(new Vector3(-149.825f, 1013.516f, 88.81807f));
            DefenseTop.Add(new Vector3(-147.45f, 1016.14f, 88.77906f));
            DefenseTop.Add(new Vector3(-144.8488f, 1018.694f, 88.81709f));
            DefenseTop.Add(new Vector3(-142.2912f, 1021.05f, 88.73335f));
            DefenseTop.Add(new Vector3(-138.9157f, 1023.9f, 88.36328f));
            DefenseTop.Add(new Vector3(-135.0411f, 1026.99f, 87.81445f));
            DefenseTop.Add(new Vector3(-131.6525f, 1029.693f, 87.46904f));
            DefenseTop.Add(new Vector3(-127.7032f, 1032.626f, 87.14428f));
            DefenseTop.Add(new Vector3(-124.5404f, 1034.328f, 86.80894f));
            DefenseTop.Add(new Vector3(-121.0399f, 1035.798f, 86.34376f));
            DefenseTop.Add(new Vector3(-117.5932f, 1036.906f, 85.98744f));
            DefenseTop.Add(new Vector3(-114.088f, 1037.419f, 85.77201f));
            DefenseTop.Add(new Vector3(-109.8923f, 1037.46f, 85.60317f));
            DefenseTop.Add(new Vector3(-106.2586f, 1037.027f, 85.55857f));
            DefenseTop.Add(new Vector3(-102.4033f, 1035.979f, 85.67416f));
            DefenseTop.Add(new Vector3(-98.75623f, 1034.755f, 85.87054f));
            DefenseTop.Add(new Vector3(-95.22954f, 1033.333f, 86.06387f));
            DefenseTop.Add(new Vector3(-92.0914f, 1031.956f, 86.19865f));
            DefenseTop.Add(new Vector3(-87.9621f, 1030.12f, 86.45184f));
            DefenseTop.Add(new Vector3(-84.21658f, 1028.454f, 86.84148f));
            DefenseTop.Add(new Vector3(-80.75591f, 1026.743f, 87.2935f));
            DefenseTop.Add(new Vector3(-77.66933f, 1024.58f, 87.80258f));
            DefenseTop.Add(new Vector3(-75.59236f, 1021.258f, 88.33041f));
            DefenseTop.Add(new Vector3(-74.49399f, 1017.16f, 88.83662f));
            DefenseTop.Add(new Vector3(-73.74346f, 1013.421f, 89.23676f));
            DefenseTop.Add(new Vector3(-73.1731f, 1009.72f, 89.58842f));
            DefenseTop.Add(new Vector3(-72.80008f, 1006.195f, 89.52682f));
            DefenseTop.Add(new Vector3(-72.40433f, 1002.537f, 89.4711f));
            DefenseTop.Add(new Vector3(-71.98147f, 998.6286f, 90.03493f));
            DefenseTop.Add(new Vector3(-71.52789f, 994.4363f, 90.74424f));
            DefenseTop.Add(new Vector3(-71.14222f, 990.8281f, 91.14565f));
            DefenseTop.Add(new Vector3(-71.06166f, 986.7139f, 91.42723f));
            DefenseTop.Add(new Vector3(-71.03955f, 982.8164f, 91.59683f));
            DefenseTop.Add(new Vector3(-71.33355f, 978.8135f, 92.14476f));
            DefenseTop.Add(new Vector3(-71.77444f, 974.7549f, 92.85664f));
            DefenseTop.Add(new Vector3(-72.21172f, 970.7299f, 93.06127f));
            DefenseTop.Add(new Vector3(-72.59818f, 967.1724f, 93.14854f));
            DefenseTop.Add(new Vector3(-73.00098f, 963.4646f, 93.64874f));
            DefenseTop.Add(new Vector3(-73.42011f, 959.6065f, 94.11914f));
            DefenseTop.Add(new Vector3(-74.08166f, 955.6828f, 94.8664f));
            DefenseTop.Add(new Vector3(-74.92001f, 951.9631f, 95.47077f));
            DefenseTop.Add(new Vector3(-76.09813f, 948.5316f, 95.94934f));
            DefenseTop.Add(new Vector3(-77.53942f, 944.8561f, 96.04788f));
            DefenseTop.Add(new Vector3(-78.90018f, 941.4017f, 96.00404f));
            DefenseTop.Add(new Vector3(-80.36559f, 937.6815f, 96.52454f));
            DefenseTop.Add(new Vector3(-81.9911f, 933.5549f, 97.12329f));
            DefenseTop.Add(new Vector3(-83.50781f, 929.7105f, 97.09819f));
            DefenseTop.Add(new Vector3(-84.98331f, 926.1759f, 97.67249f));
            DefenseTop.Add(new Vector3(-86.65633f, 922.489f, 98.07993f));
            DefenseTop.Add(new Vector3(-88.34305f, 919.6483f, 98.24454f));
            DefenseTop.Add(new Vector3(-90.82877f, 917.4499f, 98.23798f));
            DefenseTop.Add(new Vector3(-94.14989f, 915.6476f, 98.18526f));
            DefenseTop.Add(new Vector3(-97.73008f, 913.99f, 98.16344f));
            DefenseTop.Add(new Vector3(-101.1955f, 912.7073f, 98.1773f));
            DefenseTop.Add(new Vector3(-105.1191f, 911.8465f, 98.3024f));
            DefenseTop.Add(new Vector3(-108.7479f, 911.5064f, 98.39639f));
            DefenseTop.Add(new Vector3(-112.4137f, 911.8057f, 98.45927f));
            DefenseTop.Add(new Vector3(-116.1766f, 912.4253f, 98.41528f));
            DefenseTop.Add(new Vector3(-120.1209f, 913.2117f, 98.45756f));
            DefenseTop.Add(new Vector3(-124.6746f, 914.3914f, 98.36434f));
            DefenseTop.Add(new Vector3(-127.9212f, 915.311f, 98.42406f));
            DefenseTop.Add(new Vector3(-135.6208f, 918.7528f, 98.59301f));
            DefenseTop.Add(new Vector3(-139.3175f, 920.5574f, 98.61871f));
            DefenseTop.Add(new Vector3(-142.5231f, 922.3288f, 98.51097f));
            DefenseTop.Add(new Vector3(-146.0923f, 924.3438f, 98.2924f));
            DefenseTop.Add(new Vector3(-149.1446f, 926.6551f, 98.52392f));
            DefenseTop.Add(new Vector3(-152.2361f, 929.0834f, 98.55582f));
            DefenseTop.Add(new Vector3(-155.4598f, 931.6155f, 98.32745f));
            DefenseTop.Add(new Vector3(-158.1153f, 933.7015f, 98.06899f));
            DefenseTop.Add(new Vector3(-161.1664f, 936.1517f, 97.3621f));
            DefenseTop.Add(new Vector3(-163.8578f, 938.8298f, 96.27142f));
            DefenseTop.Add(new Vector3(-166.5353f, 941.498f, 95.39146f));
            DefenseTop.Add(new Vector3(-169.2129f, 944.1661f, 95.03075f));
            DefenseTop.Add(new Vector3(-171.831f, 946.775f, 94.63129f));
            DefenseTop.Add(new Vector3(-174.3537f, 949.4061f, 94.14292f));
            DefenseTop.Add(new Vector3(-176.7006f, 952.039f, 93.6119f));
            DefenseTop.Add(new Vector3(-178.6886f, 954.7052f, 93.07598f));
            DefenseTop.Add(new Vector3(-180.6999f, 957.7858f, 92.47874f));
            DefenseTop.Add(new Vector3(-182.7377f, 960.9296f, 91.88012f));

            GetOffBoat.Add(new Vector3(5.0f, 0f, 0f));
            GetOffBoat.Add(new Vector3(-4.412517f, 5.108343f, 9.437192f));
            GetOffBoat.Add(new Vector3(4.38302f, 6.148833f, 9.459474f));
            GetOffBoat.Add(new Vector3(12.08014f, 6.29997f, 5.393375f), WaypointAction.Stop);


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
            return "SOTAMovementBot";
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
        bool wasnotinbg = true;
        bool wasfighting = false;
        bool wasmoving = false;
        bool needQ = true;
        string prevloc = "";
        Vector3 targetpos;
        int jumpcount = 0;
        int failedcount = 0;
        int placecount = 0;
        public override void DoAction()
        {
            while (true)
            {

                if (Enabledcheck.Checked == false)
                {
                    placecount++;
//                     WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
//                     if (plyr != null && plyr.GUID.High != 0)
//                     {
//                         if (placecount % 30 == 0)
//                         {
//                             theform.AddMovementText(plyr.X.ToString() + " " + plyr.Y.ToString() + " " + plyr.Facing.ToString());
//                         }
//                     }
                    wasmoving = false;
                    Thread.Sleep(33);
                    continue;
                }
                try
                {
                    env.Update();

                    WoahObject plyr = (WoahObject)env.Objects[env.PlayerID];
                    if (plyr != null && plyr.GUID.High != 0)
                    {
                        ++count;

                        // determine forward vector
                        float xx = (float)Math.Cos(plyr.Facing + (Math.PI / 2));
                        float yy = (float)Math.Sin(plyr.Facing + (Math.PI / 2));
                        Vector3 forward = new Vector3(xx, yy, 0);
                        Vector3 mypos = new Vector3(plyr.X, plyr.Y, 0);
                        if(waiting)
                        {
                            if (DateTime.Now > dt)
                            { // done waiting
                                
                                waiting = false;
                                // figure out where we are
                                // DETERMINE WHICH GRAVEYARD WE SPAWNED AT

                                while (!(plyr.Facing > 0.4 && plyr.Facing < 0.7))
                                {
                                    ArrayList input = new ArrayList();
                                    input.Add(new keyinput(0x1e, true));// SPACE
                                    theform.sendline(input);
                                    Thread.Sleep(50);

                                    input = new ArrayList();
                                    input.Add(new keyinput(0x1e, false));
                                    theform.sendline(input);
                                    Thread.Sleep(200);
                                }
                                theform.moveclick();
                                Thread.Sleep(3000);
                                FindStartingWaypoint(mypos, 30);
                                //waypoints = GetOffBoat;
                                if (wasmoving == false)
                                {
                                    wasmoving = true;
                                    theform.moveclick();
                                }


                                mypos = new Vector3(plyr.X, plyr.Y, 0);
                                currentwaypoint = waypoints.FindClosestTo(mypos);
                                targetpos = currentwaypoint.location;
                                theform.AddMovementText("Done Waiting");
                                Thread.Sleep(33);
                                continue;
                            }
                            else
                            { // continue waiting
                                if (count % 50 == 0)
                                {
                                    TimeSpan ds = dt - DateTime.Now;
                                    theform.AddMovementText("continue Waiting " + ds.ToString());
                                }
                                if (count % 1000 == 0)
                                {
                                    ArrayList input = new ArrayList();
                                    input.Add(new keyinput(0x39, true));// SPACE
                                    input.Add(new keyinput(0x39, false));
                                    theform.sendline(input);
                                }
                                Thread.Sleep(33);
                                continue;

                            }
                        }

                        if (env.Location != "battleground" || env.Location == null)
                        {
                            waypoints = BeginningWait;
                            currentwaypoint = waypoints.FindClosestTo(new Vector3(plyr.X, plyr.Y, 0));

                            if (wasmoving == true)
                            {
                                theform.moveunclick();
                                wasmoving = false;
                            }

                            if (needQ)
                            {
                                theform.joinSOTA();
                                needQ = false;
                            }

                            jumpcount += 1;

                            if (jumpcount % 200 == 0)
                            {
                                ArrayList input = new ArrayList();
                                input.Add(new keyinput(0x11, true));// SPACE
                                theform.sendline(input);

                                input = new ArrayList();
                                input.Add(new keyinput(0x11, false));
                                theform.sendline(input);
                            }

                            wasnotinbg = true;
                            Thread.Sleep(66);
                            continue;
                        }
                        else if (env.Location == "battleground" && Vector3.Distance(mypos, GetOffBoat.Get(0).location) > 10 )
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
                                FindStartingWaypoint(mypos, 20);
                                WoahObject plyar = (WoahObject)env.Objects[env.PlayerID];
                                if (plyar != null)
                                {

                                    currentwaypoint = waypoints.FindClosestTo(new Vector3(plyar.X, plyar.Y, 0));
                                    targetpos = currentwaypoint.location;
                                }
                            }

                        }
                        else if (env.Location == "battleground" && Vector3.Distance(mypos, GetOffBoat.Get(0).location) < 5 && waypoints.Name == BeginningWait.Name && waiting == false)
                        {

                            theform.AddMovementText("start Waiting");
                            waiting = true;
                            dt = DateTime.Now;
                            dt = dt.AddMinutes(1.6);
                            if (wasmoving == true)
                            {
                                wasmoving = false;
                                theform.moveunclick();
                            }
                            Thread.Sleep(33);
                            continue;
                        }


                        if(waypoints.Name == BeginningWait.Name)
                        {
                            FindStartingWaypoint(mypos, 20);
                            currentwaypoint = waypoints.FindClosestTo(mypos);
                            targetpos = currentwaypoint.location;
                            theform.AddMovementText("Found waiting condition, this shouldnt be the current waypointlist");
                            Thread.Sleep(120);
                            continue;
                        }

                        // Detect offense to defense switch
                        if (waypoints.Name != DefenseStartToUpper.Name && InDefenseBox(mypos))
                        {
                            waypoints = DefenseStartToUpper;
                            currentwaypoint = waypoints.FindClosestTo(new Vector3(plyr.X, plyr.Y, 0));
                            targetpos = currentwaypoint.location;
                            Thread.Sleep(33);
                            continue;
                        }

                        failedcount = 0;
                        //theform.moveclick();

                        if (wasmoving == false)
                        {
                            wasmoving = true;
                            theform.moveclick();
                        }
                        

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

                            FindStartingWaypoint(mypos,20);
                            
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

                        if (distance < 3f && wefight == false)
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
                                if (waypoints.Name == LeftToBegGY.Name)
                                {
                                    waypoints = BegGYToLeftMid;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == RightToBegGY.Name)
                                {
                                    waypoints = BegGYToLeftMid;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == BegGYToLeftMid.Name)
                                {
                                    waypoints = LeftMidToRightMid;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == LeftMidToRightMid.Name)
                                {
                                    waypoints = RightMidToUpper;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;

                                }
                                else if (waypoints.Name == LeftMidToRightMid.Name)
                                {
                                    waypoints = RightMidToUpper;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;
                                }
                                else if (waypoints.Name == DefenseStartToUpper.Name)
                                {
                                    waypoints = DefenseUpper;
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                    Thread.Sleep(33);
                                    continue;
                                }
                                else if (waypoints.Name == GetOffBoat.Name)
                                {
                                    Thread.Sleep(400);
                                    FindStartingWaypoint(mypos,20);
                                    currentwaypoint = waypoints.FindClosestTo(mypos);
                                    targetpos = currentwaypoint.location;
                                 
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
                        if (failedcount > 5)
                        {
                            if (wasmoving == true)
                            {
                                wasmoving = false;
                                theform.moveunclick();
                            }
                            theform.AddIRCText("FAILED!");
                            theform.ReSetup();
                        }

                    }

                }
                catch (Exception ex)
                {

                }
                Thread.Sleep(16);
            }
        }

        private void FindStartingWaypoint(Vector3 mypos, float tolerance)
        {
            if (Vector3.Distance(mypos, LeftToBegGY.Get(0).location) < tolerance)
            {
                waypoints = LeftToBegGY;
            }
            else if (Vector3.Distance(mypos, RightToBegGY.Get(0).location) < tolerance)
            {
                waypoints = RightToBegGY;
            }
            else if (Vector3.Distance(mypos, BegGYToLeftMid.Get(0).location) < tolerance)
            {
                waypoints = BegGYToLeftMid;
            }
            else if (Vector3.Distance(mypos, LeftMidToRightMid.Get(0).location) < tolerance)
            {
                waypoints = LeftMidToRightMid;
            }
            else if (Vector3.Distance(mypos, RightMidToUpper.Get(0).location) < tolerance)
            {
                waypoints = RightMidToUpper;
            }
            else if (Vector3.Distance(mypos, DefenseUpper.Get(0).location) < tolerance)
            {
                waypoints = DefenseUpper;
            }
            else if (Vector3.Distance(mypos, DefenseTop.Get(0).location) < tolerance)
            {
                waypoints = DefenseTop;
            }
            
        }
        private bool InDefenseBox(Vector3 pos)
        {
            if(pos.X > -96.01175f && pos.X < -36.66162f)
            {
                if(pos.Y > 1209.747f && pos.Y < 1240.956)
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsAhead(Vector3 me, Vector3 target, Vector3 forward, float cosThreshold)
        {
            Vector3 targetDirection = (target - me);
            targetDirection.Normalize();
            return Vector3.DotProduct(forward, targetDirection) > cosThreshold;
        }
    }
}
