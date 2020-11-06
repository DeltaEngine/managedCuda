﻿//	Copyright (c) 2012, Michael Kunz. All rights reserved.
//	http://kunzmi.github.io/managedCuda
//
//	This file is part of ManagedCuda.
//
//	ManagedCuda is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Lesser General Public License as 
//	published by the Free Software Foundation, either version 2.1 of the 
//	License, or (at your option) any later version.
//
//	ManagedCuda is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//	GNU Lesser General Public License for more details.
//
//	You should have received a copy of the GNU Lesser General Public
//	License along with this library; if not, write to the Free Software
//	Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
//	MA 02110-1301  USA, http://www.gnu.org/licenses/.


using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ManagedCuda.BasicTypes
{
	#region Enums (Flags)

	/// <summary>
	/// Flags to register a graphics resource
	/// </summary>
	[Flags]
	public enum CUGraphicsRegisterFlags
	{
		/// <summary>
		/// Specifies no hints about how this resource will be used. 
		/// It is therefore assumed that this resource will be read 
		/// from and written to by CUDA. This is the default value.
		/// </summary>
		None = 0x00,
		/// <summary>
		/// Specifies that CUDA will not write to this resource.
		/// </summary>
		ReadOnly = 0x01,
		/// <summary>
		/// Specifies that CUDA will not read from this resource and 
		/// will write over the entire contents of the resource, so 
		/// none of the data previously stored in the resource will 
		/// be preserved.
		/// </summary>
		WriteDiscard = 0x02,
		/// <summary>
		/// Specifies that CUDA will bind this resource to a surface reference.
		/// </summary>
		SurfaceLDST = 0x04,
		/// <summary>
		/// 
		/// </summary>
		TextureGather = 0x08
	}

	/// <summary>
	/// Flags for mapping and unmapping graphics interop resources
	/// </summary>
	[Flags]
	public enum CUGraphicsMapResourceFlags
	{
		/// <summary>
		/// Specifies no hints about how this resource will be used.
		/// It is therefore assumed that this resource will be read from and written to by CUDA. This is the default value.
		/// </summary>
		None = 0,
		/// <summary>
		/// Specifies that CUDA will not write to this resource.
		/// </summary>
		ReadOnly = 1,
		/// <summary>
		/// Specifies that CUDA will not read from
		/// this resource and will write over the entire contents of the resource, so none of the data previously stored in the
		/// resource will be preserved.
		/// </summary>
		WriteDiscard = 2
	}

	/// <summary>
	/// CUTexRefSetFlags
	/// </summary>
	[Flags]
	public enum CUTexRefSetFlags
	{
		/// <summary>
		/// 
		/// </summary>
		None = 0,
		/// <summary>
		/// Read the texture as integers rather than promoting the values to floats in the range [0,1].
		/// <para/>Flag for <see cref="DriverAPINativeMethods.TextureReferenceManagement.cuTexRefSetFlags"/>
		/// </summary>
		ReadAsInteger = 1,

		/// <summary>
		/// Use normalized texture coordinates in the range [0,1) instead of [0,dim).
		/// <para/>Flag for <see cref="DriverAPINativeMethods.TextureReferenceManagement.cuTexRefSetFlags"/>
		/// </summary>
		NormalizedCoordinates = 2,

		/// <summary>
		/// Perform sRGB -> linear conversion during texture read.
		/// </summary>
		sRGB = 0x10,

		/// <summary>
		/// Disable any trilinear filtering optimizations.
		/// </summary>
		DisableTrilinearOptimization = 0x20
	}

	/// <summary>
	/// CUDA driver API initialization flags
	/// </summary>
	[Flags]
	public enum CUInitializationFlags : uint
	{
		/// <summary>
		/// Currently no initialization flags are defined.
		/// </summary>
		None = 0
	}

	/// <summary>
	/// CUDA driver API Context Enable Peer Access flags
	/// </summary>
	[Flags]
	public enum CtxEnablePeerAccessFlags : uint
	{
		/// <summary>
		/// Currently no flags are defined.
		/// </summary>
		None = 0
	}

	/// <summary>
	/// CUDA stream flags
	/// </summary>
	[Flags]
	public enum CUStreamFlags : uint
	{
		/// <summary>
		/// For compatibilty with pre Cuda 5.0, equal to Default
		/// </summary>
		None = 0,
		/// <summary>
		/// Default stream flag
		/// </summary>
		Default = 0x0,
		/// <summary>
		/// Stream does not synchronize with stream 0 (the NULL stream)
		/// </summary>
		NonBlocking = 0x1,
    }

    /// <summary>
    /// CudaCooperativeLaunchMultiDeviceFlags
    /// </summary>
    [Flags]
    public enum CudaCooperativeLaunchMultiDeviceFlags
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0,

        /// <summary>
        /// If set, each kernel launched as part of ::cuLaunchCooperativeKernelMultiDevice only
        /// waits for prior work in the stream corresponding to that GPU to complete before the
        /// kernel begins execution.
        /// </summary>
        NoPreLaunchSync = 0x01,

        /// <summary>
        /// If set, any subsequent work pushed in a stream that participated in a call to
        /// ::cuLaunchCooperativeKernelMultiDevice will only wait for the kernel launched on
        /// the GPU corresponding to that stream to complete before it begins execution.
        /// </summary>
        NoPostLaunchSync = 0x02,
    }

    /// <summary>
    /// CUDAArray3DFlags
    /// </summary>
    [Flags]
	public enum CUDAArray3DFlags
	{
		/// <summary>
		/// No flags
		/// </summary>
		None = 0,

		/// <summary>
		/// if set, the CUDA array contains an array of 2D slices and
		/// the Depth member of CUDA_ARRAY3D_DESCRIPTOR specifies the
		/// number of slices, not the depth of a 3D array.
		/// </summary>
		[Obsolete("Since CUDA Version 4.0. Use <Layered> instead")]
		Array2D = 1,

		/// <summary>
		/// if set, the CUDA array contains an array of layers where each layer is either a 1D
		/// or a 2D array and the Depth member of CUDA_ARRAY3D_DESCRIPTOR specifies the number
		/// of layers, not the depth of a 3D array.
		/// </summary>
		Layered = 1,

		/// <summary>
		/// this flag must be set in order to bind a surface reference
		/// to the CUDA array
		/// </summary>
		SurfaceLDST = 2,

		/// <summary>
		/// If set, the CUDA array is a collection of six 2D arrays, representing faces of a cube. The
		/// width of such a CUDA array must be equal to its height, and Depth must be six.
		/// If ::CUDA_ARRAY3D_LAYERED flag is also set, then the CUDA array is a collection of cubemaps
		/// and Depth must be a multiple of six.
		/// </summary>
		Cubemap = 4,

		/// <summary>
		/// This flag must be set in order to perform texture gather operations on a CUDA array.
		/// </summary>
		TextureGather = 8,

		/// <summary>
		/// This flag if set indicates that the CUDA array is a DEPTH_TEXTURE.
		/// </summary>
		DepthTexture = 0x10,

        /// <summary>
        /// This flag indicates that the CUDA array may be bound as a color target in an external graphics API
        /// </summary>
        ColorAttachment = 0x20,
		
		/// <summary>
		/// This flag if set indicates that the CUDA array or CUDA mipmapped array
		/// is a sparse CUDA array or CUDA mipmapped array respectively
		/// </summary>
		Sparse = 0x40
	}

	/// <summary>
	/// CUMemHostAllocFlags. All of these flags are orthogonal to one another: a developer may allocate memory that is portable, mapped and/or
	/// write-combined with no restrictions.
	/// </summary>
	[Flags]
	public enum CUMemHostAllocFlags
	{
		/// <summary>
		/// No flags
		/// </summary>
		None = 0,
		/// <summary>
		/// The memory returned by this call will be considered as pinned memory
		/// by all CUDA contexts, not just the one that performed the allocation.
		/// </summary>
		Portable = 1,

		/// <summary>
		/// Maps the allocation into the CUDA address space. The device pointer
		/// to the memory may be obtained by calling <see cref="DriverAPINativeMethods.MemoryManagement.cuMemHostGetDevicePointer_v2"/>. This feature is available only on
		/// GPUs with compute capability greater than or equal to 1.1.
		/// </summary>
		DeviceMap = 2,
		
		/// <summary>
		/// Allocates the memory as write-combined (WC). WC memory
		/// can be transferred across the PCI Express bus more quickly on some system configurations, but cannot be read
		/// efficiently by most CPUs. WC memory is a good option for buffers that will be written by the CPU and read by
		/// the GPU via mapped pinned memory or host->device transfers.<para/>
		/// If set, host memory is allocated as write-combined - fast to write,
		/// faster to DMA, slow to read except via SSE4 streaming load instruction
		/// (MOVNTDQA).
		/// </summary>
		WriteCombined = 4
	}

	/// <summary>
	/// Context creation flags. <para></para>
	/// The two LSBs of the flags parameter can be used to control how the OS thread, which owns the CUDA context at
	/// the time of an API call, interacts with the OS scheduler when waiting for results from the GPU.
	/// </summary>
	[Flags]
	public enum CUCtxFlags
	{
		/// <summary>
		/// The default value if the flags parameter is zero, uses a heuristic based on the
		/// number of active CUDA contexts in the process C and the number of logical processors in the system P. If C >
		/// P, then CUDA will yield to other OS threads when waiting for the GPU, otherwise CUDA will not yield while
		/// waiting for results and actively spin on the processor.
		/// </summary>
		SchedAuto = 0,

		/// <summary>
		/// Instruct CUDA to actively spin when waiting for results from the GPU. This can decrease
		/// latency when waiting for the GPU, but may lower the performance of CPU threads if they are performing
		/// work in parallel with the CUDA thread.
		/// </summary>
		SchedSpin = 1,

		/// <summary>
		/// Instruct CUDA to yield its thread when waiting for results from the GPU. This can
		/// increase latency when waiting for the GPU, but can increase the performance of CPU threads performing work
		/// in parallel with the GPU.
		/// </summary>
		SchedYield = 2,

		/// <summary>
		/// Instruct CUDA to block the CPU thread on a synchronization primitive when waiting for the GPU to finish work.
		/// </summary>
		BlockingSync = 4,

		/// <summary>
		/// No description found...
		/// </summary>
		SchedMask = 7,

		/// <summary>
		/// Instruct CUDA to support mapped pinned allocations. This flag must be set in order to allocate pinned host memory that is accessible to the GPU.
		/// </summary>
		[Obsolete("This flag was deprecated as of CUDA 11.0 and it no longer has any effect. All contexts as of CUDA 3.2 behave as though the flag is enabled.")]
		MapHost = 8,

		/// <summary>
		/// Instruct CUDA to not reduce local memory after resizing local memory
		/// for a kernel. This can prevent thrashing by local memory allocations when launching many kernels with high
		/// local memory usage at the cost of potentially increased memory usage.
		/// </summary>
		LMemResizeToMax = 16,

		/// <summary>
		/// No description found...
		/// </summary>
		FlagsMask = 0x1f
	}

	/// <summary>
	/// CUMemHostRegisterFlags. All of these flags are orthogonal to one another: a developer may allocate memory that is portable or mapped
	/// with no restrictions.
	/// </summary>
	[Flags]
	public enum CUMemHostRegisterFlags
	{
		/// <summary>
		/// No flags
		/// </summary>
		None = 0,
		/// <summary>
		/// The memory returned by this call will be considered as pinned memory
		/// by all CUDA contexts, not just the one that performed the allocation.
		/// </summary>
		Portable = 1,

		/// <summary>
		/// Maps the allocation into the CUDA address space. The device pointer
		/// to the memory may be obtained by calling <see cref="DriverAPINativeMethods.MemoryManagement.cuMemHostGetDevicePointer_v2"/>. This feature is available only on
		/// GPUs with compute capability greater than or equal to 1.1.
		/// </summary>
		DeviceMap = 2,

		/// <summary>
		/// If set, the passed memory pointer is treated as pointing to some
		/// memory-mapped I/O space, e.g. belonging to a third-party PCIe device.<para/>
		/// On Windows the flag is a no-op.<para/>
		/// On Linux that memory is marked as non cache-coherent for the GPU and
		/// is expected to be physically contiguous.<para/>
		/// On all other platforms, it is not supported and CUDA_ERROR_INVALID_VALUE
		/// is returned.<para/>
		/// </summary>
		IOMemory = 0x04,

		/// <summary>
		/// If set, the passed memory pointer is treated as pointing to memory that is
		/// considered read-only by the device.  On platforms without
		/// CU_DEVICE_ATTRIBUTE_PAGEABLE_MEMORY_ACCESS_USES_HOST_PAGE_TABLES, this flag is
		/// required in order to register memory mapped to the CPU as read-only.  Support
		/// for the use of this flag can be queried from the device attribute
		/// CU_DEVICE_ATTRIBUTE_READ_ONLY_HOST_REGISTER_SUPPORTED.  Using this flag with
		/// a current context associated with a device that does not have this attribute
		/// set will cause ::cuMemHostRegister to error with CUDA_ERROR_NOT_SUPPORTED.
		/// </summary>
		ReadOnly = 0x08
	}

	/// <summary>
	/// Indicates that the layered sparse CUDA array or CUDA mipmapped array has a single mip tail region for all layers
	/// </summary>
	[Flags]
	public enum CUArraySparsePropertiesFlags : uint
	{
		/// <summary>
		/// No flags
		/// </summary>
		None = 0,

		/// <summary>
		/// Indicates that the layered sparse CUDA array or CUDA mipmapped array has a single mip tail region for all layers
		/// </summary>
		SingleMiptail = 0x1
	}

	/// <summary>
	/// Flag for cuStreamAddCallback()
	/// </summary>
	[Flags]
	public enum CUStreamAddCallbackFlags
	{
		/// <summary>
		/// No flags
		/// </summary>
		None = 0x0,
		///// <summary>
		///// The stream callback blocks the stream until it is done executing.
		///// </summary>
		//Blocking = 0x01,
	}

	/// <summary>
	/// Event creation flags
	/// </summary>
	[Flags]
	public enum CUEventFlags
	{
		/// <summary>
		/// Default event creation flag.
		/// </summary>
		Default = 0,

		/// <summary>
		/// Specifies that event should use blocking synchronization. A CPU thread
		/// that uses <see cref="DriverAPINativeMethods.Events.cuEventSynchronize"/> to wait on an event created with this flag will block until the event has actually
		/// been recorded.
		/// </summary>
		BlockingSync = 1,

		/// <summary>
		/// Event will not record timing data
		/// </summary>
		DisableTiming = 2,

		/// <summary>
		/// Event is suitable for interprocess use. CUEventFlags.DisableTiming must be set
		/// </summary>
		InterProcess = 4
	}

	/// <summary>
	/// Event record flags
	/// </summary>
	[Flags]
	public enum CUEventRecordFlags : uint
	{
		/// <summary>
		/// Default event record flag
		/// </summary>
		Default = 0x0,
		/// <summary>
		/// When using stream capture, create an event record node
		/// instead of the default behavior. This flag is invalid
		/// when used outside of capture.
		/// </summary>
		External = 0x1
	}

	/// <summary>
	/// Event wait flags
	/// </summary>
	[Flags]
	public enum CUEventWaitFlags : uint
	{
		/// <summary>
		/// Default event wait flag
		/// </summary>
		Default = 0x0,
		/// <summary>
		/// When using stream capture, create an event wait node
		/// instead of the default behavior. This flag is invalid
		/// when used outside of capture.
		/// </summary>
		External = 0x1
	}

	/// <summary>
	/// Flags for ::cuStreamWaitValue32
	/// </summary>
	[Flags]
    public enum CUstreamWaitValue_flags
    {
        /// <summary>
        /// Wait until (int32_t)(*addr - value) >= 0 (or int64_t for 64 bit values). Note this is a cyclic comparison which ignores wraparound. (Default behavior.) 
        /// </summary>
        GEQ = 0x0,
        /// <summary>
        /// Wait until *addr == value.
        /// </summary>
        EQ = 0x1,
        /// <summary>
        /// Wait until (*addr &amp; value) != 0.
        /// </summary>
        And = 0x2,
        /// <summary>
        /// Wait until ~(*addr | value) != 0. Support for this operation can be
        /// queried with ::cuDeviceGetAttribute() and ::CU_DEVICE_ATTRIBUTE_CAN_USE_STREAM_WAIT_VALUE_NOR. 
        /// Generally, this requires compute capability 7.0 or greater. 
        /// </summary>
        NOr = 0x3,
        /// <summary>
        /// Follow the wait operation with a flush of outstanding remote writes. This
        /// means that, if a remote write operation is guaranteed to have reached the
        /// device before the wait can be satisfied, that write is guaranteed to be
        /// visible to downstream device work. The device is permitted to reorder
        /// remote writes internally. For example, this flag would be required if
        /// two remote writes arrive in a defined order, the wait is satisfied by the
        /// second write, and downstream work needs to observe the first write.
        /// </summary>
        Flush = 1 << 30
    }

    /// <summary>
    /// Flags for ::cuStreamWriteValue32
    /// </summary>
    [Flags]
    public enum CUstreamWriteValue_flags
    {
        /// <summary>
        /// Default behavior
        /// </summary>
        Default = 0x0,
        /// <summary>
        /// Permits the write to be reordered with writes which were issued
        /// before it, as a performance optimization. Normally, ::cuStreamWriteValue32 will provide a memory fence before the
        /// write, which has similar semantics to __threadfence_system() but is scoped to the stream rather than a CUDA thread.
        /// </summary>
        NoMemoryBarrier = 0x1
    }



    /// <summary>
    /// Indicates that the external memory object is a dedicated resource
    /// </summary>
    [Flags]
    public enum CudaExternalMemory
    {
        /// <summary>
        /// No flags
        /// </summary>
        Nothing = 0x0,
        /// <summary>
        /// Indicates that the external memory object is a dedicated resource
        /// </summary>
        Dedicated = 0x01,
    }

	/// <summary>
	/// parameter of ::CUDA_EXTERNAL_SEMAPHORE_SIGNAL_PARAMS
	/// </summary>
	[Flags]
	public enum CudaExternalSemaphore
	{
		/// <summary>
		/// When the /p flags parameter of ::CUDA_EXTERNAL_SEMAPHORE_SIGNAL_PARAMS
		/// contains this flag, it indicates that signaling an external semaphore object
		/// should skip performing appropriate memory synchronization operations over all
		/// the external memory objects that are imported as ::CU_EXTERNAL_MEMORY_HANDLE_TYPE_NVSCIBUF,
		/// which otherwise are performed by default to ensure data coherency with other
		/// importers of the same NvSciBuf memory objects.
		/// </summary>
		SignalSkipNvSciBufMemSync = 0x01,

		/// <summary>
		/// When the /p flags parameter of ::CUDA_EXTERNAL_SEMAPHORE_WAIT_PARAMS
		/// contains this flag, it indicates that waiting on an external semaphore object
		/// should skip performing appropriate memory synchronization operations over all
		/// the external memory objects that are imported as ::CU_EXTERNAL_MEMORY_HANDLE_TYPE_NVSCIBUF,
		/// which otherwise are performed by default to ensure data coherency with other
		/// importers of the same NvSciBuf memory objects.
		/// </summary>
		WaitSkipNvSciBufMemSync = 0x02,
	}

	/// <summary>
	/// flags of ::cuDeviceGetNvSciSyncAttributes
	/// </summary>
	[Flags]
	public enum NvSciSyncAttr
	{
		/// <summary>
		/// When /p flags of ::cuDeviceGetNvSciSyncAttributes is set to this,
		/// it indicates that application needs signaler specific NvSciSyncAttr
		/// to be filled by ::cuDeviceGetNvSciSyncAttributes.
		/// </summary>
		Signal = 0x01,

		/// <summary>
		/// When /p flags of ::cuDeviceGetNvSciSyncAttributes is set to this,
		/// it indicates that application needs waiter specific NvSciSyncAttr
		/// to be filled by ::cuDeviceGetNvSciSyncAttributes.
		/// </summary>
		Wait = 0x02,
	}

	/// <summary>
	/// Flags for specifying particular handle types
	/// </summary>
	[Flags]
	public enum CUmemAllocationHandleType
	{
		/// <summary>
		/// Allows a file descriptor to be used for exporting. Permitted only on POSIX systems. (int)
		/// </summary>
		PosixFileDescriptor = 0x1,  
		/// <summary>
		/// Allows a Win32 NT handle to be used for exporting. (HANDLE)
		/// </summary>
		Win32 = 0x2, 
		/// <summary>
		/// Allows a Win32 KMT handle to be used for exporting. (D3DKMT_HANDLE)
		/// </summary>
		Win32KMT = 0x4
	}

	/// <summary>
	/// Specifies the memory protection flags for mapping.
	/// </summary>
	[Flags]
	public enum CUmemAccess_flags
	{
		/// <summary>
		/// Default, make the address range not accessible
		/// </summary>
		ProtNone = 0x1,
		/// <summary>
		/// Make the address range read accessible
		/// </summary>
		ProtRead = 0x2,
		/// <summary>
		/// Make the address range read-write accessible
		/// </summary>
		ProtReadWrite = 0x3
	}
	

	/// <summary>
	/// Flag for requesting different optimal and required granularities for an allocation.
	/// </summary>
	[Flags]
	public enum CUmemAllocationGranularity_flags
	{
		/// <summary>
		/// Minimum required granularity for allocation
		/// </summary>
		Minimum = 0x0,
		/// <summary>
		/// Recommended granularity for allocation for best performance
		/// </summary>
		Recommended = 0x1
	}

	#endregion

	#region Delegates

	/// <summary>
	/// CUDA stream callback
	/// </summary>
	/// <param name="hStream">The stream the callback was added to, as passed to ::cuStreamAddCallback.  May be NULL.</param>
	/// <param name="status">CUDA_SUCCESS or any persistent error on the stream.</param>
	/// <param name="userData">User parameter provided at registration.</param>
	public delegate void CUstreamCallback(CUstream hStream, CUResult status, IntPtr userData);

	/// <summary>
	/// Block size to per-block dynamic shared memory mapping for a certain
	/// kernel.<para/>
	/// e.g.:
	/// If no dynamic shared memory is used: x => 0<para/>
	/// If 4 bytes shared memory per thread is used: x = 4 * x
	/// </summary>
	/// <param name="aBlockSize">block size</param>
	/// <returns>The dynamic shared memory needed by a block</returns>
	public delegate SizeT del_CUoccupancyB2DSize(int aBlockSize);

    /// <summary>
	/// CUDA host function
	/// </summary>
    /// <param name="userData">Argument value passed to the function</param>
    public delegate void CUhostFn (IntPtr userData);

	#endregion

}
