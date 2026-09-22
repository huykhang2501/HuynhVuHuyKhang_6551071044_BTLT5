namespace Bai3_TodoList;

public sealed class Form1 : Form
{
    private const string CompletedPrefix = "[Hoàn thành] ";
    private readonly TextBox txtCongViecMoi = new TextBox();
    private readonly ListBox lstCongViec = new ListBox();

    public Form1()
    {
        Text = "Danh sách việc cần làm hằng ngày";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(780, 620);
        MinimumSize = new Size(650, 500);
        BackColor = Color.FromArgb(248, 250, 252);
        Font = new Font("Segoe UI", 10F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Panel header = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.FromArgb(79, 70, 229) };
        header.Controls.Add(new Label { Text = "VIỆC CẦN LÀM HÔM NAY", AutoSize = true, Location = new Point(30, 16), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 22F) });
        header.Controls.Add(new Label { Text = "Nhấp chuột phải vào công việc để đánh dấu hoặc xóa", AutoSize = true, Location = new Point(34, 62), ForeColor = Color.FromArgb(224, 231, 255) });

        Panel input = new Panel { Dock = DockStyle.Top, Height = 88, BackColor = Color.White, Padding = new Padding(28, 22, 28, 18) };
        txtCongViecMoi.Dock = DockStyle.Fill;
        txtCongViecMoi.Font = new Font("Segoe UI", 11F);
        txtCongViecMoi.PlaceholderText = "Nhập công việc mới...";
        Button btnThem = new Button { Text = "Thêm", Dock = DockStyle.Right, Width = 140, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(79, 70, 229), ForeColor = Color.White, Font = new Font("Segoe UI Semibold", 10F), Cursor = Cursors.Hand, Margin = new Padding(12, 0, 0, 0) };
        btnThem.FlatAppearance.BorderSize = 0;
        btnThem.Click += delegate { AddTask(); };
        Panel spacer = new Panel { Dock = DockStyle.Right, Width = 12 };
        input.Controls.Add(txtCongViecMoi);
        input.Controls.Add(spacer);
        input.Controls.Add(btnThem);
        AcceptButton = btnThem;

        Panel listHost = new Panel { Dock = DockStyle.Fill, Padding = new Padding(28, 22, 28, 28), BackColor = Color.FromArgb(248, 250, 252) };
        lstCongViec.Dock = DockStyle.Fill;
        lstCongViec.Font = new Font("Segoe UI", 11F);
        lstCongViec.BorderStyle = BorderStyle.FixedSingle;
        lstCongViec.IntegralHeight = false;
        listHost.Controls.Add(lstCongViec);

        ContextMenuStrip cmsCongViec = new ContextMenuStrip();
        cmsCongViec.Items.Add("Đánh dấu hoàn thành", null, delegate { MarkCompleted(); });
        cmsCongViec.Items.Add("Xóa công việc này", null, delegate { DeleteSelected(); });
        cmsCongViec.Items.Add(new ToolStripSeparator());
        cmsCongViec.Items.Add("Xóa tất cả", null, delegate { DeleteAll(); });
        lstCongViec.ContextMenuStrip = cmsCongViec;
        lstCongViec.MouseDown += delegate(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = lstCongViec.IndexFromPoint(e.Location);
                lstCongViec.SelectedIndex = index;
            }
        };

        Controls.Add(listHost);
        Controls.Add(input);
        Controls.Add(header);
        Shown += delegate { txtCongViecMoi.Focus(); };
    }

    private void AddTask()
    {
        string task = txtCongViecMoi.Text.Trim();
        if (task.Length == 0)
        {
            MessageBox.Show("Vui lòng nhập nội dung công việc.", "Chưa có công việc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtCongViecMoi.Focus();
            return;
        }
        lstCongViec.Items.Add(task);
        txtCongViecMoi.Clear();
        txtCongViecMoi.Focus();
    }

    private void MarkCompleted()
    {
        if (lstCongViec.SelectedItem == null)
        {
            MessageBox.Show("Hãy chọn một công việc trước.", "Chưa chọn công việc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        int index = lstCongViec.SelectedIndex;
        string task = lstCongViec.SelectedItem.ToString() ?? string.Empty;
        if (!task.StartsWith(CompletedPrefix, StringComparison.Ordinal))
        {
            lstCongViec.Items[index] = CompletedPrefix + task;
        }
    }

    private void DeleteSelected()
    {
        if (lstCongViec.SelectedItem == null)
        {
            MessageBox.Show("Hãy chọn công việc cần xóa.", "Chưa chọn công việc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        lstCongViec.Items.RemoveAt(lstCongViec.SelectedIndex);
    }

    private void DeleteAll()
    {
        if (lstCongViec.Items.Count == 0)
        {
            MessageBox.Show("Danh sách hiện đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa tất cả công việc?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        if (result == DialogResult.Yes) lstCongViec.Items.Clear();
    }
}
