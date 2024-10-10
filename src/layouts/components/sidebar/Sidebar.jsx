import React from 'react'
import '../sidebar/Sidebar.scss';
import all_icon from '../../../assets/img/filter/icon-fabricFilter-all.jpg';

export const Sidebar = () => {
  return (
    <div className="side-menu menu-fabric" id='MnFabric'>
      <div className="left-filter">
        <ul className="filter-nenu">
        <li className="filter-item" data-tog="e-all">
          <a href="javascript:void(0);">
            <img src={{all_icon}} alt="Filter All"/> ALL
            </a>
          </li>
        </ul>
      </div>
    </div>
  )
}
