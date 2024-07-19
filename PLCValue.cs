using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPU_OTOMASYON
{
    internal class PLCValue
    {


        public PLCValue() { }


        private string m_testStep;
        public string TestStep
        {
            get { return m_testStep; }
            set
            {

                if (value != m_testStep)
                {
                    m_testStep = value;


                }
            }
        }

        private string m_voltage;
        public string Voltage
        {
            get { return m_voltage; }
            set
            {

                if (value != m_voltage)
                {
                    m_voltage = value;


                }
            }
        }
        private string m_current;
        public string Current
        {
            get { return m_current; }
            set
            {

                if (value != m_current)
                {
                    m_current = value;


                }
            }
        }


        private string m_bsBar;
        public string Bsbar
        {
            get { return m_bsBar; }
            set
            {

                if (value != m_bsBar)
                {
                    m_bsBar = value;
                    //Test.Singleton(MainForm.m_mf).textBox10.Clear();
                    if (!string.IsNullOrEmpty(m_bsBar))
                    {
                        string sülo = m_bsBar.Substring(0, 3);

                        if ((Convert.ToInt32(sülo) <= 120))
                        {


                            ElValfiForm.Instance().ControlCloseForm();

                        }
                    }

                }
            }
        }

        private string m_bsXBar;
        public string BsXbar
        {
            get { return m_bsXBar; }
            set
            {

                if (value != m_bsXBar)
                {
                    m_bsXBar = value;


                }
            }
        }

        private string m_bpBar;
        public string Bpbar
        {
            get { return m_bpBar; }
            set
            {

                if (value != m_bpBar)
                {
                    m_bpBar = value;


                }
            }
        }

        private string m_bpXBar;
        public string BpXbar
        {
            get { return m_bpXBar; }
            set
            {

                if (value != m_bpXBar)
                {
                    m_bpXBar = value;


                }
            }
        }

        private string m_asValf;
        public string AsValf
        {
            get { return m_asValf; }
            set
            {

                if (value != m_asValf)
                {
                    m_asValf = value;


                }
            }
        }

        private string m_atValf;
        public string AtValf
        {
            get { return m_atValf; }
            set
            {

                if (value != m_atValf)
                {
                    m_atValf = value;


                }
            }
        }



        private string m_emergencyBrakeValf;
        public string EmergencyBrakeValf
        {
            get { return m_emergencyBrakeValf; }
            set
            {

                if (value != m_emergencyBrakeValf)
                {
                    m_emergencyBrakeValf = value;


                }
            }
        }

        private string m_parkingBrakeValf;
        public string ParkingBrakeValf
        {
            get { return m_parkingBrakeValf; }
            set
            {

                if (value != m_parkingBrakeValf)
                {
                    m_parkingBrakeValf = value;


                }
            }
        }

        private string m_elValfiBraek120;
        public string ElValfiBraek120
        {
            get { return m_elValfiBraek120; }
            set
            {

                if (value != m_elValfiBraek120)
                {
                    m_elValfiBraek120 = value;

                    if (!string.IsNullOrEmpty(m_elValfiBraek120))
                    {
                        string sülo = m_elValfiBraek120.Substring(0, 1);

                        if ((Convert.ToInt32(sülo) <= 6))
                        {


                            ElValfiForm2.Instance().ControlCloseForm();

                        }
                    }
                }
            }
        }


        private string m_st12wr;
        public string St12wr
        {
            get { return m_st12wr; }
            set
            {

                if (value != m_st12wr)
                {
                    m_st12wr = value;


                }
            }
        }

        private string m_st13wr;
        public string St13wr
        {
            get { return m_st13wr; }
            set
            {

                if (value != m_st13wr)
                {
                    m_st13wr = value;


                }
            }
        }

        private string m_st14wr;
        public string St14wr
        {
            get { return m_st14wr; }
            set
            {

                if (value != m_st14wr)
                {
                    m_st14wr = value;


                }
            }
        }

        private string m_st15wr;
        public string St15wr
        {
            get { return m_st15wr; }
            set
            {

                if (value != m_st15wr)
                {
                    m_st15wr = value;


                }
            }
        }

        private string m_st16wr;
        public string St16wr
        {
            get { return m_st16wr; }
            set
            {

                if (value != m_st16wr)
                {
                    m_st16wr = value;


                }
            }
        }

        private string m_st30wr;
        public string St30wr
        {
            get { return m_st30wr; }
            set
            {

                if (value != m_st30wr)
                {
                    m_st30wr = value;


                }
            }
        }

        private string m_st38wr;
        public string St38wr
        {
            get { return m_st38wr; }
            set
            {

                if (value != m_st38wr)
                {
                    m_st38wr = value;


                }
            }
        }




    }
}
