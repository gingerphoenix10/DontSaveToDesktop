Ever hated how every time you save a video it goes straight to your desktop? Well now you can move them!
This mod lets you change the exact location and filename that Content Warning saves your videos to.

## Text replacements
To make sure every video has a different name, you can use these strings in your file name or path to keep them dynamic:
- %HANDLE - The handle of your video. For example, if your filename would usually be "content_warning_abcdabcd", then your handle would be "abcdabcd".
- %USER - The path to your C:\Users\user folder. Used for setting the file path. For example, the base game would use "%USER\Desktop" as a file path.
- %DAY - The total amount of days you've played so far, including quotas. For example, day 1 in quota 2 would be day 4.
- %QDAY - The total amount of days you are into the current quota.
- %RUN - The current quota you're on (like 1, 2, 3. not view quota wise).
- %YY - The current year using 2 digits. For example, if the date is January 12th 2025 9:31:52 PM, then %YY would be 25.
- %CCYY - The current year using 4 digits. For example, if the date is January 12th 2025 9:31:52 PM, then %CCYY would be 2025.
- %MM - The current month. For example, if the date is January 12th 2025 9:31:52, then %MM would be 01.
- %DD - The current (real world) day as a number. For example, if the date is January 12th 2025 9:31 PM, then %DD would be 12.
- %hh - The current hour. For example, if the date is January 12th 2025 9:31:52 PM, then %hh would be 21 (in 24-hour time. Not 12-hour).
- %mm - The current minute. For example, if the date is January 12th 2025 9:31:52 PM, then %mm would be 31.
- %ss - The current second. For example, if the date is January 12 2025 9:31:52 PM, then %ss would be 52.

## Default values:
File Name: "content_warning_%HANDLE"<br>
File Path: "%USER\Desktop"