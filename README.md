# Day-Rater
A small program in C# to assist with daily journaling

<img width="589" alt="DayRater1" src="https://github.com/JorgeAgue/Day-Rater/assets/98124296/cd8271c6-2978-4b45-b9b0-65c83869faf7">

# Features
File Loading and Saving: Using a previously created txt file, the user may load their file to be used in the Day Rater. As well, it allows the user to save any additions or changes they have made

Viewing: Allows the user to view their currently loaded journal in a larger window than normally provided

Adding a Day: Allows the user to add a new entry to their journal using the whatever text they put in the empty textbox and the rating they have selected

Changing a Day: Upon clicking an entry, the user may retroactively modify selected entry, changing text and rating for given day

Removing a Day: Upon clicking an entry, the user may remove selected entry

Unselecting: Upon clicking an entry, if the user may not want to change or remove the entry, they may press the unselect button to return to normal control

Average Rating: When loading a correctly formated journal, the average rating gathered from each individual day's rating is displayed

Setting Default File: The user may set a file to be automatically loaded when booting the program for ease of use. They may also reset the default file

# To-do
✅ Save users' loaded file, so it is automatically loaded when booting up.

✅ Add ToolTip/ErrorProviders to clarify program functions.

☐ Ask user if they are sure they want to close the program when they have unsaved changes.

✅ Fix bug where loading a new file while having a default, saves on the default file instead. 

✅ Make loading a file a separate method. 
