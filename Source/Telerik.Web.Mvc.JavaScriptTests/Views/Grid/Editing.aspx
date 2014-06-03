<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Telerik.Web.Mvc.JavaScriptTests.Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Editing</h2>
<%= Html.Telerik().Grid(Model)
            .Name("Grid1")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name).Format("<strong>{0}</strong>");
                    columns.Bound(c => c.BirthDate).Format("{0:d}");
                    columns.Bound(c => c.ReadOnly);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>

    <%= Html.Telerik().Grid(Model)
            .Name("Grid2")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.BirthDate).Format("{0:d}");
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Server()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>

    <%= Html.Telerik().Grid(Model)
            .Name("Grid3")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Active);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>
    <%= Html.Telerik().Grid(Model)
            .Name("Grid4")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Active);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>
    
    <%= Html.Telerik().Grid(Model)
            .Name("Grid5")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Active);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Pageable(pager => pager.PageSize(10))
    %>

       <%= Html.Telerik().Grid(Model)
            .Name("Grid6")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.Active);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Editable(editing => editing.Mode(GridEditMode.PopUp).Window(settings => settings.Effects(effects => effects.Toggle())))
            .Pageable(pager => pager.PageSize(10))
    %>

    <%= Html.Telerik().Grid(Model)
            .Name("Grid7")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.IntegerValue);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Editable(editing => editing.Mode(GridEditMode.InLine))
            .Pageable(pager => pager.PageSize(10))
    %>    
    
    <%= Html.Telerik().Grid(Model)
            .Name("Grid8")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.Address);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Editable(editing => editing.Mode(GridEditMode.InLine))
            .Pageable(pager => pager.PageSize(10))
    %>    
    
    <%= Html.Telerik().Grid(Model)
            .Name("Grid9")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.Address);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Editable(editing => editing.Mode(GridEditMode.InLine))
            .Pageable(pager => pager.PageSize(10))
    %>

     <%= Html.Telerik().Grid(Model)
            .Name("Grid10")
            .DataKeys(keys => keys.Add(c => c.Name))
            .ToolBar(toolbar => toolbar.Insert())
            .Columns(columns => 
                {
                    columns.Bound(c => c.Name);
                    columns.Bound(c => c.Address);
                    columns.Command(commands =>
                    {
                        commands.Edit();
                        commands.Delete();
                    });
                })
            .DataBinding(binding => binding.Ajax()
                .Select("Select", "Dummy")
                .Insert("Insert", "Dummy")
                .Delete("Delete", "Dummy")
                .Update("Update", "Dummy")
            )
            .Editable(editing => editing.Mode(GridEditMode.InLine))
            .Pageable(pager => pager.PageSize(10))
    %>

    <script type="text/javascript">
        
        function getGrid(selector) {
            return $(selector || "#Grid1").data("tGrid");
        }

        function addNew() {
            $('#Grid1 .t-grid-add').trigger('click');
        }
        
        function edit() {
            $('#Grid1 tbody tr:first').find('.t-grid-edit').trigger('click');
        }

        function cancel() {
            $('#Grid1 tbody tr:first').find('.t-grid-cancel').trigger('click');
        }

        function update() {
            $('#Grid1 tbody tr:first').find('.t-grid-update').trigger('click');
        }

        function insert() {
            $('#Grid1 tbody tr:first').find('.t-grid-insert').trigger('click');
        }
        
        function tearDown() {
            getGrid().cancel();
            getGrid().editing = { confirmDelete: true, mode: 'InLine' };
            var wnd = $('.t-window').data('tWindow');
            if (wnd) wnd.destroy();
        }

        function test_edit_for_does_not_exist_by_default() {
            assertEquals(0, $('#Grid1form').length);
        }

        function test_clicking_edit_creates_edit_form() {
            edit();
            assertEquals(1, $('#Grid1form').length);
        }
        
        function test_clicking_cancel_removes_the_edit_form() {
            edit();
            cancel();
            assertEquals(0, $('#Grid1form').length);
        }

        function test_clicking_edit_puts_the_row_in_edit_mode() {
            edit();

            assertEquals('Customer1', $('#Grid1 tbody tr').find(':input').first().val());
        }

        function test_date_is_set_according_to_format() {
            edit();
            
            assertEquals('1/1/1980', $('#Grid1 tbody tr').find(':input').eq(1).val());
        }

        function test_clicking_cancel_restores_original_data() {
            edit();
            $('#Grid1 tbody tr').find(':input').val('test');
            cancel();
            edit();
            assertEquals('Customer1', $('#Grid1 tbody tr').find(':input').first().val());
        }

        function test_data_keys_serialized() {
            assertEquals('Customer1', getGrid().data[0].Name);
            assertEquals('id', getGrid().dataKeys.Name);
        }

        function test_clicking_update_posts_data() {
            
            var called = false;

            getGrid().sendValues = function(values, url) {
                called = true;
                assertEquals('Customer1', values.Name);
                assertEquals('Customer1', values.id);
                assertEquals('updateUrl', url);
            }
            
            edit();
            update();

            assertTrue(called);
        }

        function test_create_row_creates_edit_form() {
            getGrid().addRow();

            assertEquals(1, $('#Grid1form').length);
        }

        function test_create_row_creates_empty_edit_form() {
            getGrid().addRow();

            assertEquals('', $('#Grid1 tbody tr:first :input').first().val());
        }

        function test_clicking_insert_posts_data() {
            var called = false;

            getGrid().sendValues = function(values, url) {
                called = true;
                assertEquals('test', values.Name);
                assertUndefined(values.id);
                assertEquals('insertUrl', url);
            }

            getGrid().addRow();
            
            $('#Grid1 tbody tr:first :input').val('test');
            
            insert();

            assertTrue(called);
        }

        function test_clicking_delete_posts_data() {
            var called = false;
            getGrid().sendValues = function(values, url) {
                called = true;
                assertEquals('Customer1', values.id);
                assertEquals('deleteUrl', url);
            }

            window.confirm = function() {
                return true;
            }
            $('#Grid1 tbody tr:first .t-grid-delete').trigger('click');

            assertTrue(called);
        }

        function test_no_confirmation() {
            getGrid().editing = { confirmDelete: false };

            var called = false;
            window.confirm = function() {
                called = true;
            }
            $('#Grid1 tbody tr:first .t-grid-delete').trigger('click');

            assertFalse(called);
        }

        function test_cancelling_confirm_on_delete_does_not_post_data() {
            var called = false;
            
            getGrid().sendValues = function(values, url) {
                called = true;
            }

            window.confirm = function() {
                return false;
            }

            $('#Grid1 tbody tr:first .t-grid-delete').trigger('click');

            assertFalse(called);
        }

        function test_validation() {

            var called = false;
            getGrid().sendValues = function(values, url) {
                called = true;
            }

            getGrid().addRow();
            insert();

            assertFalse(called);
            assertEquals(1, $('#Grid1 .field-validation-error').length);
        }

        function test_datepicker_instantiation() {
            addNew();

            assertNotUndefined($('div[id^=BirthDate]').data('tDatePicker'));
        }

        function test_confirm_shown_for_server_binding() {
            var grid = getGrid('#Grid2');
            var called = false;
            window.confirm = function() {
                called = true;
                return false;
            }

            $('#Grid2 tbody tr:first .t-grid-delete').trigger('click');

            assertTrue(called);
        }

        function test_read_only_column() {
            var grid = getGrid();
            assertTrue(grid.columns[2].readonly);
        }

        function test_read_only_column_shown_in_edit_mode() {
            edit();

            assertEquals("0", $('#Grid1form tr:first td:eq(2)').text());
        }

        function test_checkbox_checked_for_true_boolean() {
            $('#Grid3 tbody tr:first').find('.t-grid-edit').trigger('click');
            assertTrue($('#Grid3 tbody tr:first :checkbox').attr('checked'));
        }

        function test_extracts_boolean_values_from_checkboxes() {
            $('#Grid3 tbody tr:eq(1)').find('.t-grid-edit').trigger('click');
            $('#Grid3 tbody tr:eq(1) :checkbox').attr('checked', true);
            
            getGrid('#Grid3').ajaxRequest = function() { }
            
            var called = false;
            
            getGrid('#Grid3').sendValues = function(values, url) {
                called = true;
                assertEquals(true, values.Active);
            }

            $('#Grid3 tbody tr:eq(1)').find('.t-grid-update').trigger('click');

            assertTrue(called);
        }

        function test_cancelling_boolean_restores_the_value() {
            $('#Grid4 tbody tr:eq(0)').find('.t-grid-edit').trigger('click');
            $('#Grid4 tbody tr:eq(0)').find('.t-grid-cancel').trigger('click');

            assertEquals("true", $('#Grid4 tbody tr:eq(0) td:first').text());
        }

        function test_cancelling_raises_row_data_bound() {
            var row;
            var dataItem;
            
            $('#Grid4 tbody tr:eq(0)').find('.t-grid-edit').trigger('click');
            
            $('#Grid4').bind('rowDataBound', function(e) {
                row = e.row;
                dataItem = e.dataItem;
            });
            
            $('#Grid4 tbody tr:eq(0)').find('.t-grid-cancel').trigger('click');
            assertEquals($('#Grid4 tbody tr:eq(0)')[0], row);
            assertEquals(dataItem, $('#Grid4').data('tGrid').data[0]);
        }

        function test_booleans_are_not_validated() {
            getGrid('#Grid5').sendValues = function() { }
            $('#Grid5 tbody tr:eq(0)').find('.t-grid-edit').trigger('click');
            $('#Grid5 tbody tr:eq(0) :checkbox').attr('checked', false);
            $('#Grid5 tbody tr:eq(0)').find('.t-grid-update').trigger('click');
            assertFalse($('#Grid5 tbody tr:eq(0) :checkbox').hasClass('input-validation-error'));
        }

        function test_editing_in_popup_populates_data() {
            var grid = getGrid('#Grid6');

            $('#Grid6 tbody tr:first').find('.t-grid-edit').trigger('click');
            assertEquals('Customer1', $('#Grid6PopUp input[name=Name]').val());
            $('#Grid6PopUp').remove();
        }

        function test_editing_in_popup_update_sends_data() {
            var grid = getGrid('#Grid6');
            $('#Grid6 tbody tr:eq(1)').find('.t-grid-edit').trigger('click');
            $('#Grid6PopUp input[name=Name]').val('test');

            var called = false;
            grid.sendValues = function(values, url) {
                called = true;
                assertEquals('test', values.Name);
            }

            $('#Grid6PopUp .t-grid-update').trigger('click');

            assertTrue(called);
        }

        function test_appendCommandButtons_should_render_button_only_with_text() {
            var grid = getGrid('#Grid1');

            var builder = new $.telerik.stringBuilder();

            var command = {name:'edit', attr:'title="edit"', buttonType:'Text', imageAttr:'style="width:20px"'};
            grid.appendCommandHtml([command], builder);

            assertEquals('<a href="#" class="t-grid-action t-button t-state-default t-grid-edit" title="edit">Edit</a>', builder.string());
        }

        function test_appendCommandButtons_should_render_button_only_with_image() {
            var grid = getGrid('#Grid1');

            var builder = new $.telerik.stringBuilder();

            var command = { name: 'edit', attr: 'title="edit"', buttonType: 'Image', imageAttr: 'style="width:20px"' };
            grid.appendCommandHtml([command], builder);

            assertEquals('<a href="#" class="t-grid-action t-button t-state-default t-grid-edit" title="edit"><span class="t-icon t-edit" style="width:20px"></span></a>', builder.string());
        }

        function test_appendCommandButtons_should_render_button_only_with_image_and_text() {
            var grid = getGrid('#Grid1');

            var builder = new $.telerik.stringBuilder();

            var command = { name: 'edit', attr: 'title="edit"', buttonType: 'ImageAndText', imageAttr: 'style="width:20px"' };
            grid.appendCommandHtml([command], builder);

            assertEquals('<a href="#" class="t-grid-action t-button t-state-default t-grid-edit" title="edit"><span class="t-icon t-edit" style="width:20px"></span>Edit</a>', builder.string());
        }

        function test_nested_properties_are_set() {
            $('#Grid9 tbody tr:first').find('.t-grid-edit').trigger('click');
            
            assertEquals('foo', $('#Grid9form #Address_Street').val());
        }

        function test_extractValues_extracts_all_input_values() {
            $('#Grid8 tbody tr:first').find('.t-grid-edit').trigger('click');
            var values = getGrid('#Grid8').extractValues($('#Grid8 tbody tr:first'));
            assertEquals('foo', values['Address.Street']);
        }        
        
        function test_null_is_not_set_to_ui_elements() {
            var grid = getGrid('#Grid10');
            grid.data[7].Name = null;

            $('#Grid10 tbody tr:eq(7)').find('.t-grid-edit').trigger('click');
            assertEquals('', $('#Grid10form input[name=Name]').val());
        }
    </script>
</asp:Content>
