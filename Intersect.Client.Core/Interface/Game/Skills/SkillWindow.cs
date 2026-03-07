using System.Text;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.Layout;
using Intersect.Client.General;
using Intersect.Framework.Core.GameObjects.PlayerClass;

namespace Intersect.Client.Interface.Game.Skills
{
    public class SkillsWindow : WindowControl
    {
        private readonly ScrollControl _skillsScroller;
        private readonly RichLabel _skillsText;
        private string _lastText = string.Empty;

        public SkillsWindow(Canvas gameCanvas)
            : base(gameCanvas, "Skills", false, "SkillsWindow")
        {
            DisableResizing();
            SetSize(360, 420);
            SetPosition(500, 180);
            Title = "SKILLS";
            TitleLabel.Font = GameContentManager.Current.GetFont("sourcesansproblack");
            TitleLabel.FontSize = 16;
          
            _skillsScroller = new ScrollControl(this, nameof(_skillsScroller))
            {
                Dock = Pos.Fill,
            };

            _skillsScroller.EnableScroll(false, true);

            _skillsText = new RichLabel(_skillsScroller, nameof(_skillsText))
            {
                Dock = Pos.Fill,
                Font = GameContentManager.Current.GetFont("sourcesansproblack"),
                FontSize = 13,
                Padding = new Padding(20),
            };

            Hide();
        }

        public void SetLocation(int x, int y)
        {
            SetPosition(x, y);
        }

        public int WindowWidth => Width;
        public int WindowHeight => Height;

        public void Update()
        {
            if (IsHidden || Globals.Me == null)
            {
                return;
            }

            var playerClass = ClassDescriptor.GetName(Globals.Me.Class);

            var sb = new StringBuilder();

            sb.AppendLine($"=== {Globals.Me.Name} - Level {Globals.Me.Level} {playerClass} ===");
            sb.AppendLine();
            sb.AppendLine("COMBAT SKILLS");
            sb.AppendLine("----------------------");
            sb.AppendLine($"> Melee       : {Globals.Me.MeleeLevel}");
            sb.AppendLine($"> Shielding   : {Globals.Me.ShieldingLevel}");
            sb.AppendLine($"> Magic       : {Globals.Me.MagicLevel}");
            sb.AppendLine($"> Distance    : {Globals.Me.DistanceLevel}");
            sb.AppendLine();
            sb.AppendLine("PROFESSION SKILLS");
            sb.AppendLine("----------------------");
            sb.AppendLine($"> Mining      : {Globals.Me.MiningLevel}");
            sb.AppendLine($"> Smithing    : {Globals.Me.SmithingLevel}");
            sb.AppendLine($"> Woodcutting : {Globals.Me.WoodcuttingLevel}");
            sb.AppendLine($"> Fishing     : {Globals.Me.FishingLevel}");
            sb.AppendLine($"> Cooking     : {Globals.Me.CookingLevel}");
            sb.AppendLine($"> Crafting    : {Globals.Me.CraftingLevel}");

            var newText = sb.ToString();
            if (_lastText == newText)
            {
                return;
            }

            _lastText = newText;
            _skillsText.Text = newText;
        }
    }
}