using System;
using System.Windows.Forms;

namespace LYH.WorkOrder
{
    public static class ChildWinManage
    {
        // Methods

        public static bool ExistWin(Form mdIwin, string caption)
        {
            var flag = false;
            foreach (var form in mdIwin.MdiChildren)
            {
                if (form.Text == caption)
                {
                    flag = true;
                    form.Show();
                    form.Activate();
                    return flag;
                }
            }
            return flag;
        }

        public static Form LoadMdiForm(Form mainDialog, Type formType)
        {
            var flag = false;
            Form form = null;
            foreach (var form2 in mainDialog.MdiChildren)
            {
                if (form2.GetType() == formType)
                {
                    flag = true;
                    form = form2;
                    break;
                }
            }
            if (!flag)
            {
                form = (Form)Activator.CreateInstance(formType);
                form.MdiParent = mainDialog;
                form.Show();
            }
            form.BringToFront();
            form.Activate();
            return form;
        }

        public static void PopControlForm(Type control, string caption)
        {
            var obj2 = ReflectionUtil.CreateInstance(control);
            if (typeof(Control).IsAssignableFrom(obj2.GetType()))
            {
                var form = new Form
                {
                    WindowState = FormWindowState.Maximized,
                    ShowIcon = false,
                    Text = caption,
                    ShowInTaskbar = false,
                    StartPosition = FormStartPosition.CenterScreen
                };
                var control2 = obj2 as Control;
                control2.Dock = DockStyle.Fill;
                form.Controls.Add(control2);
                form.ShowDialog();
            }
        }

        public static void PopDialogForm(Type type)
        {
            var obj2 = ReflectionUtil.CreateInstance(type);
            if (typeof(Form).IsAssignableFrom(obj2.GetType()))
            {
                var form = obj2 as Form;
                form.ShowInTaskbar = false;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
            }
        }
    }
}
