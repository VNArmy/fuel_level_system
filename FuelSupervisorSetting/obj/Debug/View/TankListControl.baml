   M S B A M L   `   `   `  ����P  LFuelSupervisorSetting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:FuelSupervisorSetting.ViewModelFuelSupervisorSetting.ViewModel  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      0    *FuelSupervisorSetting.View.TankListControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      9local-clr-namespace:FuelSupervisorSetting.ViewModel  5      0sys&clr-namespace:System;assembly=mscorlib  5      -   5          >�Name$  
ucTankList��5	       � 	Resources 5         StaionListDataProvider S� 
ObjectType=   7FuelSupervisorSetting.ViewModel.StationListDataProvider  StationList S� 
MethodName  TankListDataProvider:   4FuelSupervisorSetting.ViewModel.TankListDataProvider  TankList S� MethodParameters��%  &	        &	      &	 [     &	 s     S� 5   
   "  6<   S� 5   
   "  5      $ GetStationList��5      5      S� 5   
   "  6:   S� 5   
   "  5      $ GetTankList��5       5      ��5      0��6   5!      5$      .��5%      � .+�5&   
   �� $��Chọn Trạm:��6   $
��Left=�6%   $��	31,34,0,0q�6@   $	��Top=�6S   �� 5'   
    �� DisplayMemberPath$ StationName��5(       �� SelectedValuePath$ 	StationId��5)      $
��Left=�5*      $��
130,34,0,0q�6.   $	��Top=�6B   $	��225��6Z   ��5'      ��  �� Source# [  �� SelectedValue 5+      �� 5,      # [ 6   	 �� Path$	 MethodParameters[0]�5-      
 �� BindsDirectlyToSource
 .  �� Mode$ OneWayToSource=�5.   /   51      )  #Microsoft.Windows.Controls.DataGrid  54   
      AutoGenerateColumns .  $
��Left=�56      $��
35,113,0,0q�6.   $	��Top=�6B   $	��377��6Z   $	��511��6g   ��54      �� # [  57        ListFuel �� Source �� RelativeSource �� AncestorType��%k   &	       �� 5:       68   �� *DataContext.ListFuel+ �� *FindAncestor+$ UserControlL�5<         Columns 5=      3  -Microsoft.Windows.Controls.DataGridTextColumn  5>      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ TankName��6(      Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$ * 6:      
IsReadOnly .  4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 6D   �� $	 TankName�6g     5?      $ Mô tả��6(   $ * 67    .   6A   �� $	 TankDesc�6c     5@      $ 
Dung tích��6(   $ * 6;    .   6E   �� $		 Max�6c   7  1Microsoft.Windows.Controls.DataGridComboBoxColumn  5A      $ 
Nhien lieu��6,   $ * 6@      DisplayMemberPath$ FuelDesc��5C   ,      SelectedValuePath$ FuelId��5D   ,      ItemsSource 5B   ,   �� # [    SelectedValueBinding 5E   ,   �� $	 FuelId�$ TwoWay=�5H      5J      5L      5M      