﻿/***************************************************************************/
/* (C) 2016 Elettra - Sincrotrone Trieste S.C.p.A.. All rights reserved.   */
/*                                                                         */
/* Copyright 2016. Elettra - Sincrotrone Trieste S.C.p.A. THE COMPANY      */
/* ELETTRA - SINCROTRONE TRIESTE S.C.P.A. IS NOT REPONSIBLE FOR THE USE    */
/* OF THIS SOFTWARE. If software is modified to produce derivative works,  */
/* such modified software should be clearly marked, so as not to confuse   */
/* it with the version available from Elettra Sincrotrone Trieste S.C.p.A. */
/*                                                                         */
/* Additionally, redistribution and use in source and binary forms, with   */
/* or without modification, are permitted provided that the following      */
/* conditions are met:                                                     */
/*                                                                         */
/*     * Redistributions of source code must retain the above copyright    */
/*       notice, this list of conditions and the following disclaimer.     */
/*                                                                         */
/*     * Redistributions in binary form must reproduce the above copyright */
/*       notice, this list of conditions and the following disclaimer in   */
/*       the documentation and/or other materials provided with the        */
/*       distribution.                                                     */
/*                                                                         */
/*     * Neither the name of Elettra - Sincotrone Trieste S.C.p.A nor      */
/*       the names of its contributors may be used to endorse or promote   */
/*       products derived from this software without specific prior        */
/*       written permission.                                               */
/*                                                                         */
/* THIS SOFTWARE IS PROVIDED BY ELETTRA - SINCROTRONE TRIESTE S.C.P.A. AND */
/* CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING,  */
/* BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND       */
/* FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL      */
/* ELETTRA - SINCROTRONE TRIESTE S.C.P.A. OR CONTRIBUTORS BE LIABLE FOR    */
/* ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL  */
/* DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE       */
/* GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS           */
/* INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER    */
/* IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR         */
/* OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF  */
/* ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.                              */
/***************************************************************************/

//
// Author: Francesco Brun
// Last modified: April, 22th 2016
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SYRMEPTomoProject.Jobs
{
    /// <summary>
    /// 
    /// </summary>
    public class ReconstructionPreviewJob : IJob
    {
        private int mImageIndex;
        private bool mPreProcess = false;
        private int mAirSx = 0;
        private int mAirDx = 0;
        private bool mFlatEnd = false;
        private bool mHalfHalf = false;
        private int mHalfHalfLine;
        private bool mExtFOV = false;
        private bool mExtFOVRight = false;
        private int mExtFOVOverlap;
        private string mRingRemoval = "-";
        private string mInputFile;
        private string mPreviewFile; 
        private double mAngles;
        private int mAngles_ProjFrom;
        private int mAngles_ProjTo;
        private double mCenter;
        private string mReconFunc; 
        private string mReconParam1; 
        private double mScale;
        private bool mZeroneMode;
        private double mCorrectionOffset;
        private bool mOverPaddding; 
        private bool mLogTransform; 
        private bool mCircle; 
        private int mDecimateFactor;
        private int mDownscaleFactor;
        private bool mPostProcess;
        private string mPostProcessConvertArgs;
        private string mPostProcessCropArgs;
        private bool mPhaseRetrieval = false;
        private double mParam1;
        private double mParam2;
        private double mDistance;
        private double mEnergy;
        private double mPixelsize;
        private bool mPhrtPad;
        private string mLogFile = Properties.Settings.Default.FormSettings_TemporaryPath +
            Path.DirectorySeparatorChar + Properties.Settings.Default.SessionID +
            Path.DirectorySeparatorChar + "_rec_log_00.txt"; // It should be "*_00.txt"


        /// <summary>
        /// 
        /// </summary>
        public string LogFile
        {
            get { return mLogFile; }
            set { mLogFile = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ReconstructionPreviewJob(
            int imageIndex,
            string inputFile,
            string previewFile,
            bool preProcess,
            int airSx,
            int airDx,
            bool flatEnd,
            bool halfHalf,
            int halfHalfLine,
            bool extFOV,
            bool extFOVRight,
            int extFOVOverlap,
            string ringRemoval,            
            double angles,
            int angles_ProjFrom,
            int angles_ProjTo,
            double center,
            string reconFunc, 
            string reconParam1,
            double scale,
            bool overPadding,
            bool logTransform,
            bool circle,
            bool zeroneMode,
            double correctionOffset,
            int decimateFactor,
            int downscaleFactor,
            bool postProcess,
            string postProcessConvertArgs,
            string postProcessCropArgs,
            bool phaseRetrieval,
            double param1,
            double param2, 
            double distance, 
            double energy, 
            double pixelsize,
            bool phrtPad
            )
        {
            mImageIndex = imageIndex;
            mPreProcess = preProcess;
            mPreviewFile = previewFile;
            mAirSx = airSx;
            mAirDx = airDx;
            mFlatEnd = flatEnd;
            mHalfHalf = halfHalf;
            mHalfHalfLine = halfHalfLine;
            mExtFOV = extFOV;
            mExtFOVRight = extFOVRight;
            mExtFOVOverlap = extFOVOverlap;
            mRingRemoval = ringRemoval;
            mInputFile = inputFile;           
            mAngles = angles;
            mAngles_ProjFrom = angles_ProjFrom;
            mAngles_ProjTo = angles_ProjTo;
            mCenter = center;
            mReconParam1 = reconParam1;
            mScale = scale;
            mOverPaddding = overPadding;
            mLogTransform = logTransform;            
            mCircle = circle;
            mZeroneMode = zeroneMode;
            mCorrectionOffset = correctionOffset;
            mReconFunc = reconFunc;
            mDecimateFactor = decimateFactor;
            mDownscaleFactor = downscaleFactor;
            mPostProcess = postProcess;
            mPostProcessConvertArgs = postProcessConvertArgs;
            mPostProcessCropArgs = postProcessCropArgs;
            mPhaseRetrieval = phaseRetrieval;
            mParam1 = param1;
            mParam2 = param2;
            mDistance = distance;
            mEnergy = energy;
            mPixelsize = pixelsize;
            mPhrtPad = phrtPad;
        }

        /// <summary>
        /// 
        /// </summary>
        public ReconstructionPreviewJob(
            int imageIndex,
            string inputFile,
            string previewFile,
            bool preProcess,
            int airSx,
            int airDx,
            bool flatEnd,
            bool halfHalf,
            int halfHalfLine,
            bool extFOV,
            bool extFOVRight,
            int extFOVOverlap,
            string ringRemoval,            
            double angles,
            int angles_ProjFrom,
            int angles_ProjTo,
            double center,
            string reconFunc,
            string reconParam1,
            double scale,
            bool overPadding,
            bool logTransform,
            bool circle,
            bool zeroneMode,
            double correctionOffset,           
            int decimateFactor,
            int downscaleFactor,
            bool postProcess,
            string postProcessConvertArgs,
            string postProcessCropArgs,
            bool phaseRetrieval,
            double param1,
            double param2, 
            double distance, 
            double energy, 
            double pixelsize,
            bool phrtPad,
            string logFile
            )
            : this(imageIndex, inputFile, previewFile, preProcess, airSx, airDx, flatEnd, halfHalf, halfHalfLine, extFOV, extFOVRight, extFOVOverlap, 
                ringRemoval, angles, angles_ProjFrom, angles_ProjTo, center, reconFunc, reconParam1, scale, overPadding, logTransform, circle, 
                zeroneMode, correctionOffset, decimateFactor, downscaleFactor, postProcess, postProcessConvertArgs, postProcessCropArgs, 
                phaseRetrieval, param1, param2, distance, energy, pixelsize, phrtPad)         
        {
            mLogFile = Properties.Settings.Default.FormSettings_TemporaryPath +
                Path.DirectorySeparatorChar + Properties.Settings.Default.SessionID +
                Path.DirectorySeparatorChar + logFile;
        }

        /// <summary>
        /// Get the command line for current Phase Retrieval filter instance.
        /// </summary>
        /// <returns>The string to execute.</returns>
        public string GetCommandLine()
        {
            //
            // python reconstruct_astra.py 310 312 "D:\\test\\patchTravan_nophrt_flathalf.h5" "R:\\" 3.14159 -113.0 shepp-logan 1.0 False True 
            // True sino slice tomo flat dark R:\\ E:\\Brun\\BrunTomoProject\\matlab_def\\MatlabAutomation_Reconstruct.exe recon_astra_fbp
            //
            //

            string zString = string.Empty; 

            // Create output path if does not exist:
            Directory.CreateDirectory(Properties.Settings.Default.FormSettings_OutputPath);

            zString = "\"" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar
                + Properties.Settings.Default.PythonPath + Path.DirectorySeparatorChar + Properties.Settings.Default.ReconstructPreviewJob + "\" "
                + mImageIndex.ToString() + " \"" +
                mInputFile + "\" \"" +
                mPreviewFile + "\" " +
                mAngles.ToString(CultureInfo.InvariantCulture) + " " +
                mCenter.ToString(CultureInfo.InvariantCulture) + " " +
                mReconParam1.ToString() + " " +
                mScale.ToString(CultureInfo.InvariantCulture) + " " +
                mOverPaddding.ToString() + " " +
                mLogTransform.ToString() + " " +
                mCircle.ToString() + " " +
                mPreProcess.ToString() + " " +                 
                mFlatEnd.ToString() + " " + mHalfHalf.ToString() + " " +
                mHalfHalfLine.ToString() + " " +
                mExtFOV.ToString() + " " + mExtFOVRight.ToString() + " " +
                mExtFOVOverlap.ToString() + " " + 
                mAirSx.ToString() + " " + mAirDx.ToString() + " False \"" +  // This has to be modified...
                mRingRemoval + "\" " +
                mZeroneMode.ToString() + " " +
                mCorrectionOffset.ToString() + " " +
                mReconFunc + " " +
                mDecimateFactor.ToString() + " " +
                mDownscaleFactor.ToString() + " " +
                mPostProcess.ToString() + " " +
                mPostProcessConvertArgs + " " +
                mPostProcessCropArgs + " " +
                mPhaseRetrieval.ToString() + " " +
                mParam1.ToString(CultureInfo.InvariantCulture) + " " +
                mParam2.ToString(CultureInfo.InvariantCulture) + " " +
                mEnergy.ToString(CultureInfo.InvariantCulture) + " " +
                mDistance.ToString(CultureInfo.InvariantCulture) + " " +
                mPixelsize.ToString(CultureInfo.InvariantCulture) + " " +
                mPhrtPad.ToString() + " " +
                Properties.Settings.Default.FormSettings_ChunkSize.ToString() + " " +
                mAngles_ProjFrom.ToString() + " " +
                mAngles_ProjTo.ToString() + 
                " True 1 \"" +
                Properties.Settings.Default.FormSettings_TemporaryPath
                + Path.DirectorySeparatorChar + Properties.Settings.Default.SessionID
                + "\" \"" +
                mLogFile + "\"";

            return zString;
        }

        /// <summary>
        /// Return a descriptor for current job.
        /// </summary>
        /// <returns>A string with the description of the job.</returns>
        public override string ToString()
        {
            return "tomographic reconstruction";
        }
    }
}
