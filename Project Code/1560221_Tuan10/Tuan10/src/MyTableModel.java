
//import java.awt.List;
import java.util.List;
import javax.swing.table.AbstractTableModel;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author hoanghuy
 */
public class MyTableModel extends AbstractTableModel {
    String[] columnNames = {"Mã số SV", "Họ và Tên", "Địa chỉ", "Điện Thoaị", "Khoa"};
    List<SinhVien> lst;
    
    public MyTableModel(List<SinhVien> lst){
        this.lst = lst;      
    }
    @Override
    public int getRowCount() {
        return lst.size();
    }
    
    
    @Override
    public String getColumnName(int col){
        return columnNames[col];
    }

    @Override
    public int getColumnCount() {
        return 5;
    }

    @Override
    public Object getValueAt(int rowIndex, int columnIndex) {
        switch(columnIndex)
        {
            case 0:
                return lst.get(rowIndex).getMssv();
            case 1:
                return lst.get(rowIndex).getHoTen();
            case 2:
                return lst.get(rowIndex).getDienThoai();
            case 3:
                return lst.get(rowIndex).getDiaChi();
            case 4:
                return lst.get(rowIndex).getKhoa();
        }
        return null;    
    }
    
}
