#!/usr/bin/env python
import google_images_download   #importing the library


from .__init__ import main

if __name__ == '__main__':
    response = google_images_download.googleimagesdownload()   #class instantiation
    arguments = {"keywords":"Polar bears,baloons,Beaches","limit":20,"print_urls":True}   #creating list of arguments
    paths = response.download(arguments)   #passing the arguments to the function
    print(paths) 