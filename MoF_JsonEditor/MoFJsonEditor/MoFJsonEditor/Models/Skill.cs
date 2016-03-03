namespace MoFJsonEditor.Models
{
    public class Skill
    {
        //public Image Image { get; set; }
        public string Name { get; set; }
        //this will be Melee or Magic
        public string SkillType { get; set; }
        //Damage the skill will do
        public int Damage { get; set; }
        //Want to have it set up to do Healing as well later.
        public int Healing { get; set; }
    }
}
