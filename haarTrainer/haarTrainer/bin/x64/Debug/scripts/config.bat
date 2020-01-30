@echo off
cd scripts
python google_images_download.py -k cat -l 400 -s ">800*600" -f jpg --chromedriver C:\Users\Parker\Desktop\tensor_flow\haarTrainer\haarTrainer\haarTrainer\bin\Debug\scripts\chromedriver.exe
pause
