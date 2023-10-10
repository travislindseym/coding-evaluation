using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;

        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        private static int nextIdentifier = 1;
        public Position? Hire(Name person, string title)
        {
            Position positionToFill = FindPosition(root, title);
            if (positionToFill == null)
            {
                return null;
            }
            else if (positionToFill.IsFilled())
            {
                throw new Exception("Position is already filled");
            }
            else
            {
                Employee newEmployee = new Employee(nextIdentifier++, person);
                positionToFill.SetEmployee(newEmployee);
                return positionToFill;
            }
        }
        private Position? FindPosition(Position position, string title)
        {
            if (position.GetTitle() == title)
            {
                return position;
            }
            else
            {
                foreach (Position directReport in position.GetDirectReports())
                {
                    Position foundPosition = FindPosition(directReport, title);
                    if (foundPosition != null)
                    {
                        return foundPosition;
                    }
                }
                return null;
            }
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
