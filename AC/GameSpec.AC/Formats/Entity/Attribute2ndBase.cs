using System.IO;

namespace GameSpec.AC.Formats.Entity
{
    public class Attribute2ndBase
    {
        public readonly SkillFormula Formula;

        public Attribute2ndBase(BinaryReader r)
            => Formula = new SkillFormula(r);
    }
}
