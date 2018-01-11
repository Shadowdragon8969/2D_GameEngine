using GameUtil;
using GameWorld;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerScienceUtilities
{
    public class Client
    {
        private static bool p = false;
        private static bool j = false;
        private static HashMap fileData = new HashMap();
        private static Client o;
        private FileInfo o1;
        private FileStream t;
        private String s;
        private static List<String> f = new List<String>();
        private static EntityPlayer cpr;
        /**
         * 
         * Developer's Note:
         * I have completely de-obfuscated 
         * this entire class in order to prevent
         * the use of unneeded methods on your side.
         * I do not plan on renaming the obfuscated methods but if you can
         * figure out what they do go ahead and rename them.
         * 
         */

        ///
        public Client(FileInfo df_04, String yd_97)
        {
            if (p)
            {
                return;
            }
            this.o1 = df_04;
            this.t = o1.Open(FileMode.Open);
            this.s = yd_97;
            o = this;
            cpr = new EntityPlayer();
            registerClient();
            func_79762();
        }

        /// <summary>
        /// Returns the player bound to the client
        /// </summary>
        /// <returns>The Client Player</returns>
        public EntityPlayer getClientPlayer()
        {
            return cpr;
        }

        /// <summary>
        /// Sets the Client Player to the Player specified
        /// </summary>
        /// <param name="ep">The Player to set the Client to</param>
        public void setClientPlayer(EntityPlayer ep)
        {
            cpr = ep;
        }

        /// <summary>
        /// Set the defaut configurations to a List<String>
        /// </summary>
        /// <param name="du_23">The List to set the defaults to</param>
        public static void setDefaults(List<String> du_23)
        {
            f = du_23;
        }

        /// <summary>
        /// Add a default to the defualt configurations
        /// </summary>
        /// <param name="su_54">The defualt to add</param>
        public static void addDefault(String su_54)
        {
            f.Add(su_54);
        }

        /// <summary>
        /// Returns the list of default configurations
        /// </summary>
        /// <returns>A list of configurations</returns>
        public static List<String> getDefualts()
        {
            return f;
        }

        /// <summary>
        /// Returns all of the lines in the runtime file
        /// </summary>
        /// <returns>The Lines in the runtime file</returns>
        public String[] getAllLines()
        {
            t.Close();
            String[] ral = File.ReadAllLines(s);
            t.Close();
            return ral;
        }

        /// <summary>
        /// Builds the Client information and writes/creates any files if needed
        /// </summary>
        /// <returns>An instance of the Client</returns>
        public static Client getClient()
        {
            if (!p)
            {
                String desktop = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (!System.IO.Directory.GetDirectories(desktop).Contains("ClientData"))
                {
                    System.IO.Directory.CreateDirectory(desktop + "\\" + "ClientData");
                }
                String dataPath = desktop + "\\" + "ClientData\\runTimeLog.txt";
                FileInfo clientData = new FileInfo(dataPath);
                if (!clientData.Exists)
                {
                    FileStream fs = File.Create(dataPath);
                    j = true;
                    fs.Close();
                }
                return new Client(clientData, dataPath);
            }
            else
            {
                return o;
            }
        }

        /// <summary>
        /// Writes the default configurations to the runtime file if it isn't already there
        /// </summary>
        public void registerClient()
        {
            if (j)
            {
                foreach (String str in getDefualts())
                {
                    addLine(str);
                }
            }
            j = false;
        }

        public String reformatText(String text)
        {
            return text.Replace('a', '[').Replace('b', '.').Replace('c', '<').Replace('d', ']').Replace('e', '?').Replace('f', '$')
            .Replace('g', '^').Replace('h', '/').Replace('i', '"').Replace('j', '#').Replace('k', '˧').Replace('l', '+').Replace('m', '{').Replace('n', '~')
            .Replace('o', '*').Replace('p', '@').Replace('q', '&').Replace('r', '%').Replace('s', '!').Replace('t', '`').Replace('u', '-')
            .Replace('v', '§').Replace('w', '=').Replace('x', '©').Replace('y', '¶').Replace('z', '˥');
        }

        public String unformatText(String text)
        {
            return text.Replace('[', 'a').Replace('.', 'b').Replace('<', 'c').Replace(']', 'd').Replace('?', 'e').Replace('$', 'f')
            .Replace('^', 'g').Replace('/', 'h').Replace('"', 'i').Replace('#', 'j').Replace('˧', 'k').Replace('+', 'l').Replace('{', 'm').Replace('~','n')
            .Replace('*', 'o').Replace('@', 'p').Replace('&', 'q').Replace('%', 'r').Replace('!', 's').Replace('`', 't').Replace('-', 'u')
            .Replace('§', 'v').Replace('=', 'w').Replace('©', 'x').Replace('¶', 'y').Replace('˥', 'z');
        }

        /// <summary>
        /// Add a new line the runtime file with the text specified
        /// </summary>
        /// <param name="a4_1">The text to write</param>
        public void addLine(String a4_1)
        {
            t.Close();
            List<String> sts = File.ReadAllLines(s).ToList();
            sts.Add(reformatText(a4_1));
            File.WriteAllLines(s, sts.ToArray());
        }

        public void writeLine(String d0_96, int tj_42)
        {
            t.Close();
            String[] sts = File.ReadAllLines(s);
            if (tj_42 > sts.Length) return;
            if (tj_42 <= sts.Length)
            {
                int skip = tj_42 - 1;
                sts.SetValue(reformatText(d0_96), skip);
                File.WriteAllLines(s, sts);
            }
        }

        public void createStream()
        {
            t.Close();
            FileStream fs = File.Create(s);
            fs.Close();

        }

        public String getLine(int top_po)
        {
            t.Close();
            String[] sts = File.ReadAllLines(s);
            return unformatText((String)sts.GetValue(top_po - 1));
        }

        private void func_79762()
        {
            t.Close();
            String[] st = File.ReadAllLines(s);
            String op = "";
            foreach (String sts in st)
            {
                if (op.Length > 1)
                {
                    op += ";" + sts;
                }
                if (op.Length < 1)
                {
                    op += sts;
                }
                
            }
            String[] ext = op.Split(';');
            foreach (String stss in ext)
            {
                if (stss.Contains(':'))
                {
                    String[] fnl = stss.Split(':');
                    fileData.put(fnl[0], fnl[1]);
                }
            }
        }

        public String getSection(String id)
        {
            for (int i = 1; i < getAllLines().Length; i ++)
            {
                if (getLine(i).Contains(id))
                {
                    String[] sts = getLine(i).Split(':');
                    return unformatText(sts[1]);
                }
            }
            return "hi";
        }

        public void writeSection(String section, String value)
        {
            bool found = false;
            for (int i = 1; i < getAllLines().Length; i ++)
            {
                if (getLine(i).Contains(section) && !found)
                {
                    writeLine(section + ":" + reformatText(value), i);
                    found = true;
                }
                if (i == getAllLines().Length - 1 && !found)
                {
                    addLine(section + ":" + reformatText(value));
                }
            }
        }

    }
}
