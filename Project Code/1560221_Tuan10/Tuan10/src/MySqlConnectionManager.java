
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.List;
import java.util.ArrayList;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
/**
 *
 * @author hoanghuy
 */
public class MySqlConnectionManager {

    public int Xoa(String MSSV){
        return MSSV;
    }
    
    public Connection getConnection() {
        Connection conn = null;

        try {
            Properties connectionProps = new Properties();
            connectionProps.put("user", "root");
            connectionProps.put("password", "");
            String ConnectionString = "jdbc:mysql://localhost:3306/QuanLySinhVienDB";
            conn = DriverManager.getConnection(ConnectionString, connectionProps);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return conn;
    }

    /**
     *
     * @return
     */
    public List<SinhVien> getAll() {
        Connection conn = getConnection();
        String query = "SELECT * FROM SinhVien";
        ResultSet rs = null;
        List<SinhVien> lst = new ArrayList<>();
        try {
            if (conn != null) {
                Statement st = conn.createStatement();
                rs = st.executeQuery(query);
                SinhVien sv = null;
                while (rs.next()) {
                    sv = new SinhVien();
                    sv.setMssv(rs.getString("Mssv"));
                    sv.setHoTen(rs.getString("HoTen"));
                    sv.setDienThoai(rs.getString("DienThoai"));
                    sv.setDiaChi(rs.getString("DiaChi"));
                    sv.setKhoa(rs.getString("Khoa"));
                    lst.add(sv);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            if (conn != null) {
                try {
                    rs.close();
                    conn.close();
                } catch (SQLException ex) {
                    Logger.getLogger(DanhSVFrom.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
        return lst;
    }
}
