import React, { useState, useEffect, useRef } from 'react'
import { LuUpload } from "react-icons/lu";
import { IoChevronDown, IoSearch, IoClose } from "react-icons/io5";
import LineCharts from '../../../chart/LineChart'; // Adjust the import path as needed
import ZoneConsumption from '../../../chart/zoneConsumption/ZoneConsumption';

export default function Reports() {
    const [billsData, setBillsData] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [showFilterDropdown, setShowFilterDropdown] = useState(false);
    const [selectedFilter, setSelectedFilter] = useState('Name');
    const [searchValue, setSearchValue] = useState('');
    const filterDropdownRef = useRef(null);
    const rowsPerPage = 5;

    useEffect(() => {
        const fetchBills = async () => {
            try {
                const response = await fetch('http://localhost:5042/api/CollageApp/students/all');
                if (!response.ok) {
                    throw new Error('Failed to fetch bills');
                }
                const result = await response.json();
                setBillsData(result);
                setLoading(false);
            } catch (err) {
                setError(err.message);
                setLoading(false);
            }
        };

        fetchBills();
    }, []);

    // Close dropdown when clicking outside
    useEffect(() => {
        const handleClickOutside = (event) => {
            // Close filter dropdown when clicking outside
            if (showFilterDropdown && filterDropdownRef.current && !filterDropdownRef.current.contains(event.target)) {
                setShowFilterDropdown(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
    }, [showFilterDropdown]);

    // Calculate pagination values
    const totalPages = Math.ceil(billsData.length / rowsPerPage);
    const startIndex = (currentPage - 1) * rowsPerPage;
    const currentRows = billsData.slice(startIndex, startIndex + rowsPerPage);

    const handleFilterToggle = () => {
        setShowFilterDropdown(!showFilterDropdown);
    };

    const handleFilterSelect = (filter) => {
        setSelectedFilter(filter);
        setShowFilterDropdown(false);
    };

    const handleSearchChange = (e) => {
        setSearchValue(e.target.value);
    };

    const handleSearch = () => {
        console.log('Search for:', searchValue, 'in', selectedFilter);
        // Implement search functionality here for the chart
    };

    const handleReset = () => {
        setSelectedFilter('Name');
        setSearchValue('');
    };

    const handleNext = () => {
        if (currentPage < totalPages) {
            setCurrentPage(currentPage + 1);
        }
    };

    const handlePrev = () => {
        if (currentPage > 1) {
            setCurrentPage(currentPage - 1);
        }
    };

    const handlePageClick = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    // Generate page numbers with ellipsis - current page + 2 neighbors
    const getPageNumbers = () => {
        const pageNumbers = [];

        if (totalPages <= 5) {
            // Show all pages if total pages is 5 or less
            for (let i = 1; i <= totalPages; i++) {
                pageNumbers.push(i);
            }
        } else {
            // Always show first page
            pageNumbers.push(1);

            // Calculate the range around current page
            let startPage = Math.max(2, currentPage - 1);
            let endPage = Math.min(totalPages - 1, currentPage + 1);

            // Add ellipsis after first page if there's a gap
            if (startPage > 2) {
                pageNumbers.push('...');
            }

            // Add pages around current page (current page + 1 before + 1 after)
            for (let i = startPage; i <= endPage; i++) {
                pageNumbers.push(i);
            }

            // Add ellipsis before last page if there's a gap
            if (endPage < totalPages - 1) {
                pageNumbers.push('...');
            }

            // Always show last page
            pageNumbers.push(totalPages);
        }

        return pageNumbers;
    };

    // Get column names from the first data item
    const columns = billsData.length > 0 ? Object.keys(billsData[0]) : [];

    if (loading) return <div className="text-gray-900 dark:text-white">Loading...</div>;
    if (error) return <div className="text-red-600 dark:text-red-400">Error: {error}</div>;

    return (
        <div className="bg-white dark:bg-gray-700 min-h-screen">
            <div className="p-4">
                <div className='text-2xl font-bold mb-4 flex justify-start text-gray-900 dark:text-white'>
                    Reports and Analytics
                </div>

                {/* Chart Section */}
                <div className="bg-white dark:bg-gray-800 rounded-xl shadow-md dark:shadow-gray-700/30 p-6 my-8">
                    <div className="flex justify-between items-center mb-6">
                        <div className="text-xl text-gray-800 dark:text-white">
                            Trend of Energy Usage over Time
                        </div>
                    </div>
                    <div className="mt-4">
                        <div className="bg-gray-50 dark:bg-gray-700 rounded-lg border border-gray-200 dark:border-gray-600 p-4">
                            <LineCharts width={1000} height={400} />
                        </div>
                    </div>
                </div>

                {/* Chart Section with Search */}
                <div className="bg-white dark:bg-gray-800 rounded-xl shadow-md dark:shadow-gray-700/30 p-6 my-8">
                    <div className="flex justify-between items-center mb-6">
                        <div className="text-xl text-gray-800 dark:text-white">
                            Compare Zone Consumption
                        </div>
                    </div>
                    
                    {/* Finder by Name Section */}
                    <div className="flex mb-6 w-full max-w-2xl" ref={filterDropdownRef}>
                        {/* Dropdown Button */}
                        <div className="relative flex-shrink-0 w-32">
                            <button
                                onClick={handleFilterToggle}
                                className="bg-white dark:bg-gray-800 border border-r-0 border-gray-300 dark:border-gray-600 rounded-l-lg px-4 py-2 flex items-center justify-between w-full text-gray-700 dark:text-gray-300 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors"
                            >
                                <span className="truncate">{selectedFilter}</span>
                                <IoChevronDown className="w-4 h-4 flex-shrink-0" />
                            </button>
                            
                            {/* Dropdown Menu */}
                            {showFilterDropdown && (
                                <div className="absolute top-full left-0 mt-1 w-48 bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-md shadow-lg z-10">
                                    {columns.map((column) => (
                                        <button
                                            key={column}
                                            onClick={() => handleFilterSelect(column)}
                                            className="w-full text-left px-4 py-2 text-sm text-gray-700 dark:text-gray-300 hover:bg-violet-100 dark:hover:bg-violet-900 transition-colors first:rounded-t-md last:rounded-b-md"
                                        >
                                            {column.charAt(0).toUpperCase() + column.slice(1)}
                                        </button>
                                    ))}
                                </div>
                            )}
                        </div>
                        
                        {/* Search Input Container */}
                        <div className="flex flex-1">
                            {/* Search Input */}
                            <input
                                type="text"
                                placeholder={`Search by ${selectedFilter.toLowerCase()}...`}
                                value={searchValue}
                                onChange={handleSearchChange}
                                className="flex-1 bg-white dark:bg-gray-800 border border-l-0 border-r-0 border-gray-300 dark:border-gray-600 px-4 py-2 text-gray-700 dark:text-gray-300 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                            />
                            
                            {/* Search Button */}
                            <button
                                onClick={handleSearch}
                                className="bg-white dark:bg-gray-800 border border-l-0 border-r-0 border-gray-300 dark:border-gray-600 px-3 py-2 text-gray-600 dark:text-gray-400 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors"
                            >
                                <IoSearch className="w-5 h-5" />
                            </button>
                            
                            {/* Reset Button */}
                            <button
                                onClick={handleReset}
                                className="bg-white dark:bg-gray-800 border border-l-0 border-gray-300 dark:border-gray-600 rounded-r-lg px-3 py-2 text-gray-600 dark:text-gray-400 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors"
                            >
                                <IoClose className="w-5 h-5" />
                            </button>
                        </div>
                    </div>

                    <div className="mt-4">
                        <div className="bg-gray-50 dark:bg-gray-700 rounded-lg border border-gray-200 dark:border-gray-600 p-4">
                            <ZoneConsumption />
                        </div>
                    </div>
                </div>

                {/* Reports Section with Export buttons */}
                <div className="flex justify-between items-center mb-6">
                    <div className='text-xl font-bold text-gray-900 dark:text-white'>
                        Reports
                    </div>
                    <div className="flex gap-4">
                        <button className='border-2 border-black dark:border-gray-400 rounded-2xl px-3 py-2 w-48 bg-white dark:bg-gray-800 text-black dark:text-white hover:bg-gray-50 dark:hover:bg-gray-700 flex items-center justify-center gap-2'>
                            <LuUpload className="w-5 h-5 text-blue-500" /> Export as CSV
                        </button>
                        <button className='border-2 border-black dark:border-gray-400 rounded-2xl px-3 py-2 w-48 bg-white dark:bg-gray-800 text-black dark:text-white hover:bg-gray-50 dark:hover:bg-gray-700 flex items-center justify-center gap-2'>
                            <LuUpload className="w-5 h-5 text-blue-500" /> Export as PDF
                        </button>
                    </div>
                </div>

                <div className="overflow-x-auto">
                    <table className="min-w-full bg-white dark:bg-gray-800 border border-gray-900 dark:border-gray-700">
                        <thead>
                            <tr className="bg-gray-100 dark:bg-gray-800">
                                {columns.map((column) => (
                                    <th key={column} className="py-3 px-4 border-b border-r border-gray-900 dark:border-gray-600 text-left font-semibold last:border-r-0 text-gray-900 dark:text-white">
                                        {column.charAt(0).toUpperCase() + column.slice(1)}
                                    </th>
                                ))}
                            </tr>
                        </thead>
                        <tbody>
                            {currentRows.map((row, index) => (
                                <tr key={index} className="hover:bg-gray-50 dark:hover:bg-gray-700">
                                    {columns.map((column) => (
                                        <td key={column} className="py-3 px-4 border-b border-r border-gray-900 dark:border-gray-600 last:border-r-0 text-gray-900 dark:text-white">
                                            {row[column]}
                                        </td>
                                    ))}
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>

                {/* Pagination Controls */}
                <div className="flex justify-between items-center mt-4">
                    <button
                        onClick={handlePrev}
                        disabled={currentPage === 1}
                        className={`px-4 py-2 rounded ${currentPage === 1
                            ? 'bg-gray-300 dark:bg-gray-700 cursor-not-allowed text-gray-500 dark:text-gray-400'
                            : 'bg-gray-600 hover:bg-gray-700 dark:bg-gray-600 dark:hover:bg-gray-500 text-white'
                            }`}
                    >
                        Previous
                    </button>

                    {/* Page Number Buttons */}
                    <div className="flex space-x-2">
                        {getPageNumbers().map((page, index) => (
                            page === '...' ? (
                                <span key={`ellipsis-${index}`} className="px-3 py-2 text-gray-500 dark:text-gray-400">
                                    ...
                                </span>
                            ) : (
                                <button
                                    key={page}
                                    onClick={() => handlePageClick(page)}
                                    className={`px-3 py-2 rounded text-sm ${currentPage === page
                                        ? 'bg-gray-800 dark:bg-gray-600 text-white'
                                        : 'bg-gray-200 hover:bg-gray-300 dark:bg-gray-700 dark:hover:bg-gray-600 text-gray-700 dark:text-gray-300'
                                        }`}
                                >
                                    {page}
                                </button>
                            )
                        ))}
                    </div>

                    <button
                        onClick={handleNext}
                        disabled={currentPage === totalPages}
                        className={`px-4 py-2 rounded ${currentPage === totalPages
                            ? 'bg-gray-300 dark:bg-gray-700 cursor-not-allowed text-gray-500 dark:text-gray-400'
                            : 'bg-gray-600 hover:bg-gray-700 dark:bg-gray-600 dark:hover:bg-gray-500 text-white'
                            }`}
                    >
                        Next
                    </button>
                </div>
            </div>
        </div>
    )
}

