<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%= Html.Telerik().Grid(Model)
            .Name("Grid")
            .DataKeys(keys => keys.Add(c => c.IntegerValue))
            .Columns(columns => 
            {
                columns.Bound(c => c.Name);
                columns.Command(commands => 
                {
                    commands.Edit();
                    commands.Delete();
                });
            })
            .Editable(settings => settings.DisplayDeleteConfirmation(false))
            .ToolBar(commands => commands.Insert())
            .DataBinding(binding => binding.Ajax().Select("foo", "bar").Update("foo","bar").Insert("foo","bar").Delete("foo", "bar"))
            .ClientEvents(events => events.OnLoad("onLoad").OnEdit("onEdit").OnSave("onSave").OnDelete("onDelete"))
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid3")
            .DataKeys(keys => keys.Add(c => c.IntegerValue))
            .Columns(columns => 
            {
                columns.Bound(c => c.Name);
                columns.Command(commands => commands.Edit());
            })
            .ToolBar(commands => commands.Insert())
            .DataBinding(binding => binding.Ajax().Select("foo", "bar").Update("foo","bar").Insert("foo","bar").Delete("foo", "bar"))
            .ClientEvents(events => events.OnSave("cancelSave"))
            .Pageable(pager => pager.PageSize(10))
    %>
    
    <%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.IntegerValue))
            .ToolBar(commands => commands.Insert())
            .Columns(columns => 
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.Active);
                columns.Command(commands => commands.Edit());
            })
            .DataBinding(binding => binding.Ajax().Select("foo", "bar").Update("foo", "bar").Insert("foo", "bar").Delete("foo", "bar"))
            .ClientEvents(events => events.OnEdit("onEdit").OnSave("onSave"))
            .Editable(editing => editing.Mode(GridEditMode.InForm))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .DataKeys(keys => keys.Add(c => c.IntegerValue))
            .ToolBar(commands => commands.Insert())
            .Columns(columns => 
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.Active); 
                columns.Command(commands => commands.Edit());
            })
            .DataBinding(binding => binding.Ajax().Select("foo", "bar").Update("foo", "bar").Insert("foo", "bar").Delete("foo", "bar"))
            .ClientEvents(events => events.OnEdit("onEdit").OnSave("onSave"))
            .Editable(editing => editing.Mode(GridEditMode.PopUp))
    %>
    <% Html.Telerik().Grid(Model)
            .Name("Grid4")
            .Columns(columns =>
            {
                columns.Bound(c => c.Name);
                columns.Bound(c => c.Active);
            })
            .ClientEvents(events => events.OnDetailViewExpand("onDetailViewExpand").OnDetailViewCollapse("onDetailViewCollapse"))
            .DataBinding(binding => binding.Ajax().Select("foo", "bar"))
            .DetailView(detailView => detailView.Template(c =>
                {
                    %> Details for :<%= c.Name %> <%
                }))
           .Render();
    %>

    <script type="text/javascript">
        var onLoadGrid;
        var onEditArguments;
        var onSaveArguments;
        var onDeleteArguments;
        var onDetailViewExpandArguments;
        var onDetailViewCollapseArguments;

        function setUp() {
            onDetailViewCollapseArguments = onDetailViewExpandArguments = onDeleteArguments = onSaveArguments = onEditArguments  = undefined;
            getGrid().sendValues = function() {};
            getGrid('#Grid1').sendValues = function() {};
            getGrid('#Grid2').sendValues = function() {};
        }
        
        function getGrid(selector) {
            
            var grid = $(selector || '#Grid').data('tGrid');
            return grid;
        }
        
        function onDelete(e) {
            e.preventDefault();
            onDeleteArguments = e;
        }

        function onLoad() {
            onLoadGrid = $(this).data('tGrid');
        }
        
        function onEdit(e) {
            onEditArguments = e;    
        }

        function onSave(e) {
            onSaveArguments = e;
        }

        function cancelSave(e) {
            e.preventDefault();
        }

        function test_client_object_is_available_in_on_load() {
            assertNotNull(onLoadGrid);
            assertNotUndefined(onLoadGrid);
        }

        function test_clicking_edit_raises_onEdit_inline_mode() {
            $('#Grid .t-grid-edit:first').click();
            assertNotUndefined(onEditArguments);
            assertEquals('edit', onEditArguments.mode);
            assertNotUndefined(onEditArguments.form);
            assertEquals(getGrid().data[0], onEditArguments.dataItem);
        }
        
        function test_clicking_add_raises_onEdit_inline_mode() {
            $('#Grid .t-grid-add:first').click();
            assertNotUndefined(onEditArguments);
            assertEquals('insert', onEditArguments.mode);
            assertNotUndefined(onEditArguments.form);
        }

        function test_clicking_save_raises_onSave_inline_mode() {
            $('#Grid .t-grid-edit:first').click();
            $('#Grid .t-grid-update:first').click();
            assertNotUndefined(onSaveArguments);
            assertNotUndefined(onSaveArguments.form);
            assertEquals('edit', onSaveArguments.mode);
            assertNotUndefined(onSaveArguments.dataItem);
            assertNotUndefined(onSaveArguments.values);
        }

        function test_cancelling_save_prevents_send_values() {
            var called = false;
            getGrid('#Grid3').sendValues = function() {
                called=true;
            }
            $('#Grid3 .t-grid-edit:first').click();
            $('#Grid3 .t-grid-update:first').click();
            assertFalse(called);
        }
        function test_cancelling_insert_prevents_send_values() {
            var called = false;
            getGrid('#Grid3').sendValues = function () {
                called = true;
            }
            $('#Grid3 .t-grid-add:first').click();
            $('#Grid3form #Name').val('test');
            $('#Grid3 .t-grid-insert:first').click();
            assertFalse(called);
        }

        function test_clicking_insert_raises_onSave_inline_mode() {
            $('#Grid .t-grid-add:first').click();
            $('#Gridform #Name').val('test');
            $('#Grid .t-grid-insert:first').click();
            assertNotUndefined(onSaveArguments);
            assertEquals('insert', onSaveArguments.mode);
            assertNotUndefined(onSaveArguments.form);
            assertNotUndefined(onSaveArguments.values);
        }

        function test_clicking_edit_raises_onEdit_informs_mode() {
            $('#Grid1 .t-grid-edit:first').click();
            assertNotUndefined(onEditArguments);
            assertNotUndefined(onEditArguments.form);
            assertEquals(getGrid('#Grid1').data[0], onEditArguments.dataItem);
        }

        function test_clicking_add_raises_onEdit_informs_mode() {
            $('#Grid1 .t-grid-add:first').click();
            assertNotUndefined(onEditArguments);
            assertNotUndefined(onEditArguments.form);
        }
        
        function test_clicking_save_raises_onSave_informs_mode() {
            $('#Grid1 .t-grid-edit:first').click();
            $('#Grid1 .t-grid-update:first').click();
            assertNotUndefined(onSaveArguments);
            assertNotUndefined(onSaveArguments.form);
            assertEquals(getGrid('#Grid1').data[0], onSaveArguments.dataItem);
            assertNotUndefined(onSaveArguments.values);
        }

        function test_clicking_insert_raises_onSave_informs_mode() {
            $('#Grid1 .t-grid-add:first').click();
            $('#Grid1form #Name').val('test');
            $('#Grid1 .t-grid-insert:first').click();
            assertNotUndefined(onSaveArguments);
            assertNotUndefined(onSaveArguments.form);
            assertNotUndefined(onSaveArguments.values);
        }

        function test_clicking_edit_raises_onEdit_popup_mode() {
            $('#Grid2 .t-grid-edit:first').click();
            assertNotUndefined(onEditArguments);
            assertNotUndefined(onEditArguments.form);
            assertEquals(getGrid('#Grid2').data[0], onEditArguments.dataItem);
        }
        
        function test_clicking_add_raises_onEdit_popup_mode() {
            $('#Grid2 .t-grid-add:first').click();
            assertNotUndefined(onEditArguments);
            assertNotUndefined(onEditArguments.form);
        }

        function test_clicking_save_raises_onSave_popup_mode() {
            $('#Grid2 .t-grid-edit:first').click();
            $('#Grid2form .t-grid-update:first').click();
            assertNotUndefined(onSaveArguments);
            assertNotUndefined(onSaveArguments.form);
            assertEquals(getGrid('#Grid2').data[0], onSaveArguments.dataItem);
            assertNotUndefined(onSaveArguments.values);
        }

        function test_clicking_insert_raises_onSave_popup_mode() {
            $('#Grid2 .t-grid-add:first').click();
            $('#Grid2form #Name').val('test');
            $('#Grid2form .t-grid-insert:first').click();
            assertNotUndefined(onSaveArguments);
            assertNotUndefined(onSaveArguments.form);
            assertNotUndefined(onSaveArguments.values);
        }

        function test_clicking_delete_raises_on_delete() {
            $('#Grid .t-grid-delete:first').click();
            assertNotUndefined(onDeleteArguments);
            assertNotUndefined(onDeleteArguments.dataItem);
        }

        function test_cancelling_delete() {
            var called = false;
            getGrid('#Grid').sendValues = function () {
                called = true;
            }
            $('#Grid .t-grid-delete:first').click();
            assertFalse(called);
        }

        function onDetailViewExpand(e) {
            onDetailViewExpandArguments = e;
        }
        
        function onDetailViewCollapse(e) {
            onDetailViewCollapseArguments = e;
        }

        function test_detail_view_expand() {
            $('#Grid4 .t-plus:first').click();
            assertNotUndefined(onDetailViewExpandArguments);
            assertEquals(getGrid('#Grid4').$tbody[0].rows[0], onDetailViewExpandArguments.masterRow);
            assertEquals(getGrid('#Grid4').$tbody[0].rows[1], onDetailViewExpandArguments.detailRow);
        }

        function test_detail_view_collapse() {
            $('#Grid4 .t-plus:first').click();
            $('#Grid4 .t-minus:first').click();
            assertNotUndefined(onDetailViewCollapseArguments);
            assertEquals(getGrid('#Grid4').$tbody[0].rows[0], onDetailViewCollapseArguments.masterRow);
            assertEquals(getGrid('#Grid4').$tbody[0].rows[1], onDetailViewCollapseArguments.detailRow);
        }
    </script>
</asp:Content>
