package com.google.atap.tangocloudservice;

import android.location.Location;
import android.os.IInterface;
import android.os.RemoteException;

interface ITangoCloudService extends IInterface {
    void registerCallbacks(ITangoCloudServiceCallbacks callbacks) throws RemoteException;

    void updateLocation(Location location) throws RemoteException;

    abstract class Stub implements ITangoCloudService {
    }
}