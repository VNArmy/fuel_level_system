   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      P LFuelSupervisorSetting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:FuelSupervisorSetting.ViewModelFuelSupervisorSetting.ViewModel 5      .   (FuelSupervisorSetting.View.Interpolation   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6dg-http://schemas.microsoft.com/wpf/2008/toolkit  5      0sys&clr-namespace:System;assembly=mscorlib  5      9local-clr-namespace:FuelSupervisorSetting.ViewModel  5      -   5          >�Name$  uc��5	       � 	Resources 5         StationListDataProvider S� 
ObjectType=  7FuelSupervisorSetting.ViewModel.StationListDataProvider  StationList S� 
MethodName  TankListDataProvider:  4FuelSupervisorSetting.ViewModel.TankListDataProvider  TankList S� MethodParameters   InterpolationTabDataProviderB  <FuelSupervisorSetting.ViewModel.InterpolationTabDataProvider  InterpolationTab �� Member��%�  &	        &	      &	 [     &	 s     &	 �     &	 �     S� 5   
   "  6=   S� 5   
   "  5      $ GetStationList��5      5      S� 5   
   "  6:   S� 5   
   "  5      $ GetTankList��5       5      ��5      0��6   5       S� 5#   
   "  6B   S� 5%   
   "  5'      $ GetInterpolationTab��5(       5)      ��5*      0��6   �� 5+      $ sys:String.Empty��6   5-      50      .��51      � .+�52   
   ]� .+�53      � $	��Top=�6   .+�54      �� $��Chọn Trạm:��6   $
��Left=�6-   $��	31,34,0,0q�6H   $	��Top=�6[   �� 55       �� DisplayMemberPath$ StationName��56       �� SelectedValuePath$ 	StationId��57      $
��Left=�58      $��
130,34,0,0q�6.   $	��Top=�6B   $	��138��6Z   ��55      ��  �� Source# [ 	 �� SelectedValue	 59      �� 5:      # [ 6"   
 �� Path$
 MethodParameters[0]�5;   "    �� BindsDirectlyToSource .  �� Mode$ OneWayToSource=�5<   ?   5>      �� 5?      $��Chọn Bồn:��6   $
��Left=�6,   $��	31,65,0,0q�6G   $	��Top=�6Z   �� 5@      -   5        $��
cbListTank��5@      $ TankName��5A      $ TankId��5B      $
��Left=�5C      $��
130,69,0,0q�6.   $	��Top=�6B   $	��138��6Z   ��5@   -   �� # [ 	 5D      �� 5E      # [ 6"   $
 MethodParameters[0]�5F   "    . $ OneWayToSource=�5G   ?   5K      �� 5L      -   5        $��txtFileName��5L      $��27��6/   $
��Wrap=�6;   $	��Top=�6P   $��31,96,117,0q�6h   {�.  ��5M      �� 5N      # [ 6"   $
 MethodParameters[1]�5O   "    . $ OneWayToSource=�5P   ?   5R      �� 5S      -   5        $��	btnImport��5S      $��	Browse...��6*   $
��Left=�6>   $	��Top=�6Y   $��75��6q    J� RenderTransformOrigin$ 3.286,-9.455&�6|   $��27��6�   $��Bottom=�6�   $��
288,96,0,0q�6�   6�   5U      )  #Microsoft.Windows.Controls.DataGrid  5V      -   5          Name$ InterpolationGrid��5W      $	��Top=�5V      $
��Left=�5X      $��	31,20,0,0q�65   $	��Top=�6H   $	��170��6`   $	��334��6p      AutoGenerateColumns .  $	��102��5Y   6   ��5W   5   �� # [    Columns 5[      3  -Microsoft.Windows.Controls.DataGridTextColumn  5]      /  )Microsoft.Windows.Controls.DataGridColumn   Header$ Level��6,      Width8  2Microsoft.Windows.Controls.DataGridLengthConverter$ 63 6;      
IsReadOnly .  4  .Microsoft.Windows.Controls.DataGridBoundColumn   Binding 6F   �� $
 Level�6e     5^      $ Capacity��6,   $ * 6>    .   6H   �� $
 Capacity�6j   5b      5c      5e      5f      