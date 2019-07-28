import numpy as np
import pandas as pd
from PIL import Image

def consolidate_data():
    jsonFile = "./training_GT_labels_v2.json"
    print('Reading GT values from ' + jsonFile)
    jsonDf = pd.read_json(jsonFile)
    img_name = pd.Series(list(jsonDf))
    transDf = jsonDf.transpose()
    gt_vals = transDf[0]
    #print(transDf.tail())
    #print(type(gt_vals))
    #print(gt_vals.head())
    dfToWrite = pd.DataFrame({'image_name' : pd.Series(list(jsonDf)), 'gt_values' : gt_vals.values})
    imgList = list(jsonDf)
    jsonDf = 0
    transDf = 0
    #print(dfToWrite.head())
    print('Starting image preprocess')
    #splitdf1 = dfToWrite[:5000]
    #splitdf2 = dfToWrite[5000:]
    imageArr = []
    count = 1
    total = str(len(imgList))
    for imgFile in imgList:
        print('Getting image ' + str(count) + ' of ' + total + ': ' + imgFile, end = '\r')
        gateImage = Image.open('./Data_Training/' + imgFile)
        gScaleGateImage = gateImage.convert("L")
        gScaleImageArray = np.asarray(gScaleGateImage)
        imageArr.append(gScaleImageArray)
        count += 1

    print("\nJoining json and image data")
    dfToWrite["image_data"] = pd.Series(imageArr)
    imageArr = []
    #print(dfToWrite.head())
    #print(dfToWrite.tail())
    #print(dfToWrite[5:15])

    return dfToWrite

#wrtPath = './combined_data_1'
#print("Writing to " + wrtPath + '.pickle')
#splitdf1.to_pickle(wrtPath + '.pickle');
#splitdf1 = 0

#for imgFile in imgList[5000:]:
#    print('Getting image ' + str(count) + ' of ' + total + ': ' + imgFile, end = '\r')
#    gateImage = Image.open('./Data_Training/' + imgFile)
#    gScaleGateImage = gateImage.convert("L")
#    gScaleImageArray = np.asarray(gScaleGateImage)
#    imageArr.append(gScaleImageArray)
#    count += 1

#print("\nJoining json and image data")
#splitdf2["image_data"] = pd.Series(imageArr)
#imageArr = []
#print(splitdf2.head())
#print(splitdf2.tail())
#print(splitdf2[5:15])

#wrtPath = './combined_data_2'
#print("Writing to " + wrtPath + '.pickle')
#splitdf2.to_pickle(wrtPath + '.pickle');
#print("Finished!!")

#readPath = './combined_data_1'
#splitdf1 = pd.read_pickle(wrtPath + '.pickle')
#readPath = './combined_data_2'
#splitdf2 = pd.read_pickle(wrtPath + '.pickle')
#print(splitdf1.head())
#print(splitdf1.tail())
#print(splitdf2.head())
#print(splitdf2.tail())
#print(dfToWrite['image_data'][499])
