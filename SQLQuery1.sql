SELECT ModuleName, ModuleCode 
FROM Modules
INNER JOIN CourseModules
On Modules.ModuleID = CourseModules.ModuleID
WHERE CourseID = 4 
ORDER BY ModuleName