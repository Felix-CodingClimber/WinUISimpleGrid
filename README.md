# WinUISimpleGrid
Basic DataGrid control for WinUI 3. Fewer features than other controls, but much more understandable for simple Scenarios or Beginners.
Important! There will be no direct editing support. The projects where I will use the DataGrid enable editing of the data only with custom item editing dialogs, so there is no need for me to implement editing directly in the DataGrid. This is one reason the controls logic can be kept relatively simple.

### Work in Progress!

If I reach a usable state, the control will be published as nuget package.

### Features supported
- [x] Displaying properties of objects in a items source in a grid.
- [x] Displaying column information in a header.
- [x] Column types: Text
- [ ] Custom column types ???
- [ ] Selection modes: Single cell, multiple cells, single line, multiple lines.
- [ ] Data sorting per column/ property.
- [ ] Column width modes: Auto, auto + resizable, manual resizable with initial width (Min and max width is supported in all modes).
- [ ] Pagination of rows. As there will be no virtualization implemented, there will be a maximum number of visible rows at a time. To help with this "problem" the DataGrid will support Pagination out of the box.
- [ ] Row and cell coloring based on rules.
