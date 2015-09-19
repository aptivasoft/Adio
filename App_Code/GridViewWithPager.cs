using System;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace App_Code
{
	public class GridViewWithPager: GridView
	{
		public bool UseCustomPager
		{
			get { return (bool?) ViewState["UseCustomPager"] ?? false; }
			set { ViewState["UseCustomPager"] = value; }
		}

		protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
		{
			if (UseCustomPager)
				CreateCustomPager(row, columnSpan, pagedDataSource);
			else
				base.InitializePager(row, columnSpan, pagedDataSource);
		}

		protected virtual void CreateCustomPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
		{
			int pageCount = pagedDataSource.PageCount;
			int pageIndex = pagedDataSource.CurrentPageIndex + 1;
			int pageButtonCount = PagerSettings.PageButtonCount;

			TableCell cell = new TableCell();
			row.Cells.Add(cell);
			if (columnSpan > 1) cell.ColumnSpan = columnSpan;

			if (pageCount > 1)
			{
				HtmlGenericControl pager = new HtmlGenericControl("div");
				pager.Attributes["class"] = "pagination";
				cell.Controls.Add(pager);

				int min = pageIndex - pageButtonCount;
				int max = pageIndex + pageButtonCount;

				if (max > pageCount)
					min -= max - pageCount;
				else if (min < 1)
					max += 1 - min;

				// Create "previous" button
				Control page = pageIndex > 1
								? BuildLinkButton(pageIndex - 2, PagerSettings.PreviousPageText, "Page", "Prev")
								: BuildSpan(PagerSettings.PreviousPageText, "disabled");
				pager.Controls.Add(page);

				// Create page buttons
				bool needDiv = false;
				for (int i = 1; i <= pageCount; i++)
				{
					if (i <= 2 || i > pageCount - 2 || (min <= i && i <= max))
					{
						string text = i.ToString(NumberFormatInfo.InvariantInfo);
						page = i == pageIndex
								? BuildSpan(text, "current")
								: BuildLinkButton(i - 1, text, "Page", text);
						pager.Controls.Add(page);
						needDiv = true;
					}
					else if (needDiv)
					{
						page = BuildSpan("&hellip;", null);
						pager.Controls.Add(page);
						needDiv = false;
					}
				}

				// Create "next" button
				page = pageIndex < pageCount
						? BuildLinkButton(pageIndex, PagerSettings.NextPageText, "Page", "Next")
						: BuildSpan(PagerSettings.NextPageText, "disabled");
				pager.Controls.Add(page);
			}
		}

		private Control BuildLinkButton(int pageIndex, string text, string commandName, string commandArgument)
		{
			PagerLinkButton link = new PagerLinkButton(this);
			link.Text = text;
			link.EnableCallback(ParentBuildCallbackArgument(pageIndex));
			link.CommandName = commandName;
			link.CommandArgument = commandArgument;
			return link;
		}

		private Control BuildSpan(string text, string cssClass)
		{
			HtmlGenericControl span = new HtmlGenericControl("span");
			if (!String.IsNullOrEmpty(cssClass)) span.Attributes["class"] = cssClass;
			span.InnerHtml = text;
			return span;
		}

		private string ParentBuildCallbackArgument(int pageIndex)
		{
			MethodInfo m =
				typeof (GridView).GetMethod("BuildCallbackArgument", BindingFlags.NonPublic | BindingFlags.Instance, null,
				                            new Type[] {typeof (int)}, null);
			return (string) m.Invoke(this, new object[] {pageIndex});
		}
	}
}