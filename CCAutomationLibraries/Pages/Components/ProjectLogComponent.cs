using System;
using CCWebUIAuto.PrimitiveElements;

namespace CCWebUIAuto.Pages.Components
{
	public class ProjectLogComponent : RoomComponent, IDynamicResultSetView
	{
		public Button BtnPrevPage { get; set; }
		public Button BtnNextPage { get; set; }
		public TextBox TxtPage { get; set; }
		public TextBox TxtRowsPerPage { get; set; }
		public Link LnkAdvanced { get; set; }
		public Button BtnGo { get; set; }
		public Button BtnClear { get; set; }

		public ProjectLogComponent(String displayName)
			: base(displayName)
		{
			this.InitializeDrsv();
		}
	}
}
