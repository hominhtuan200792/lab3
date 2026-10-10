using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3_fix
{   
        class nguoi
        {
            public string hoten { get; set; }
            public DateTime ngaysinh { get; set; }

            public nguoi(string hoTen, DateTime ngaySinh)
            {
                hoten = hoTen;
                ngaysinh = ngaySinh;
            }

            public virtual string laythongtin()
            {
                return $"Ho ten: {hoten}, Ngay sinh: {ngaysinh.ToShortDateString()}";
            }
        }

        class sinhvien : nguoi
        {
            public string masinhvien { get; set; }
            public double diemtrungbinh { get; set; }
            public string malop { get; set; }

            public sinhvien(string hoTen, DateTime ngaySinh, string maSV, double diemTB, string maLop)
                : base(hoTen, ngaySinh)
            {
                masinhvien = maSV;
                malop = maLop;
                diemtrungbinh = (diemTB < 0 || diemTB > 10) ? 0 : diemTB;
            }

            public string xeploai()
            {
                if (diemtrungbinh >= 8) return "Gioi";
                if (diemtrungbinh >= 6.5) return "Kha";
                if (diemtrungbinh >= 5) return "Trung binh";
                return "Yeu";
            }

            public override string laythongtin()
            {
                return $"Ma SV: {masinhvien}, {base.laythongtin()}, Lop: {malop}, DTB: {diemtrungbinh}, Xep loai: {xeploai()}";
            }
        }

        class quanlysinhvien
        {
            private List<sinhvien> danhsachsinhvien = new List<sinhvien>();

            public void themsinhvien(sinhvien sv)
            {
                if (danhsachsinhvien.Any(s => s.masinhvien == sv.masinhvien))
                    Console.WriteLine("Ma sinh vien da ton tai. Khong the them sinh vien.");
                else
                {
                    danhsachsinhvien.Add(sv);
                    Console.WriteLine("Them sinh vien thanh cong.");
                }
            }

            public void suasinhvien(string maSV, string hoTen, double diemTB)
            {
                var sv = danhsachsinhvien.FirstOrDefault(s => s.masinhvien == maSV);
                if (sv != null)
                {
                    sv.hoten = hoTen;
                    sv.diemtrungbinh = diemTB;
                    Console.WriteLine("Sua thong tin sinh vien thanh cong.");
                }
                else
                    Console.WriteLine("Khong tim thay sinh vien co ma tuong ung.");
            }

            public void xoasinhvien(string maSV)
            {
                var sv = danhsachsinhvien.FirstOrDefault(s => s.masinhvien == maSV);
                if (sv != null)
                {
                    danhsachsinhvien.Remove(sv);
                    Console.WriteLine("Xoa sinh viên thanh cong.");
                }
                else
                    Console.WriteLine("Khong tim thay sinh vien co ma tuong ung.");
            }

            public sinhvien timtheoma(string maSV)
            {
                return danhsachsinhvien.FirstOrDefault(s => s.masinhvien == maSV);
            }

            public List<sinhvien> timtheoten(string hoTen)
            {
                return danhsachsinhvien
                    .Where(s => s.hoten.ToLower().Contains(hoTen.ToLower()))
                    .ToList();
            }

            public void sapxeptheodiem()
            {
                danhsachsinhvien = danhsachsinhvien.OrderByDescending(s => s.diemtrungbinh).ToList();
                Console.WriteLine("Da sap xep theo diem giam dan.");
            }

            public List<sinhvien> laydanhsach()
            {
                return danhsachsinhvien;
            }
        }

        class Program
        {
            static quanlysinhvien quanLy = new quanlysinhvien();

            static void Main(string[] args)
            {
                bool thoat = false;
                while (!thoat)
                {
                    HienThiMenu();
                    string luaChon = Console.ReadLine();

                    switch (luaChon)
                    {
                        case "1": NhapThemSinhVien(); break;
                        case "2": NhapXuatDanhSach(); break;
                        case "3": NhapSuaSinhVien(); break;
                        case "4": NhapXoaSinhVien(); break;
                        case "5": NhapTimTheoMa(); break;
                        case "6": NhapTimTheoTen(); break;
                        case "7": quanLy.sapxeptheodiem(); break;
                        case "0":
                            thoat = true;
                            Console.WriteLine("Tam biet!");
                            break;
                        default:
                            Console.WriteLine("Lua chon khong hop le!");
                            break;
                    }

                    if (!thoat)
                    {
                        Console.WriteLine("\nNhan Enter de tiep tuc...");
                        Console.ReadLine();
                    }
                }
            }

            static void HienThiMenu()
            {
                Console.Clear();
                Console.WriteLine("===== QUAN LY SINH VIEN =====");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Xuat danh sach");
                Console.WriteLine("3. Sua sinh vien");
                Console.WriteLine("4. Xoa sinh vien");
                Console.WriteLine("5. Tim theo ma sinh vien");
                Console.WriteLine("6. Tim theo ten");
                Console.WriteLine("7. Sap xep theo diem");
                Console.WriteLine("0. Thoat");
                Console.Write("Chon chuc nang: ");
            }

            static void NhapThemSinhVien()
            {
                Console.Clear();
                Console.WriteLine("=== THEM SINH VIEN ===");

                Console.Write("Ma sinh vien: ");
                string ma = Console.ReadLine();
                Console.Write("Ho ten: ");
                string ten = Console.ReadLine();
                Console.Write("Ngay sinh (dd/MM/yyyy): ");
                DateTime ngaySinh = DateTime.Parse(Console.ReadLine());
                Console.Write("Ma lop: ");
                string lop = Console.ReadLine();
                Console.Write("Diem trung binh (0-10): ");
                double diem = double.Parse(Console.ReadLine());

                sinhvien sv = new sinhvien(ten, ngaySinh, ma, diem, lop);
                quanLy.themsinhvien(sv);
            }

            static void NhapXuatDanhSach()
            {
                Console.Clear();
                Console.WriteLine("=== DANH SACH SINH VIEN ===\n");

                var ds = quanLy.laydanhsach();
                if (ds.Count == 0)
                    Console.WriteLine("Danh sach rong!");
                else
                    foreach (var sv in ds)
                    {
                        Console.WriteLine(sv.laythongtin());
                        Console.WriteLine(new string('-', 60));
                    }
            }

            static void NhapSuaSinhVien()
            {
                Console.Clear();
                Console.Write("Nhap ma sinh vien can sua: ");
                string ma = Console.ReadLine();
                Console.Write("Ho ten moi: ");
                string tenMoi = Console.ReadLine();
                Console.Write("Diem trung binh moi (0-10): ");
                double diemMoi = double.Parse(Console.ReadLine());

                quanLy.suasinhvien(ma, tenMoi, diemMoi);
            }

            static void NhapXoaSinhVien()
            {
                Console.Clear();
                Console.Write("Nhap ma sinh vien can xoa: ");
                string ma = Console.ReadLine();
                quanLy.xoasinhvien(ma);
            }

            static void NhapTimTheoMa()
            {
                Console.Clear();
                Console.Write("Nhap ma sinh vien can tim: ");
                string ma = Console.ReadLine();

                sinhvien sv = quanLy.timtheoma(ma);
                if (sv != null)
                    Console.WriteLine(sv.laythongtin());
                else
                    Console.WriteLine("Khong tim thay sinh vien co ma: " + ma);
            }

            static void NhapTimTheoTen()
            {
                Console.Clear();
                Console.Write("Nhap ten can tim: ");
                string ten = Console.ReadLine();

                var ketqua = quanLy.timtheoten(ten);
                if (ketqua.Count > 0)
                {
                    Console.WriteLine("Ket qua tim kiem:");
                    foreach (var sv in ketqua)
                        Console.WriteLine(sv.laythongtin());
                }
                else
                    Console.WriteLine("Khong tim thay sinh vien co ten tuong ung.");
            }
        }
    }

    
