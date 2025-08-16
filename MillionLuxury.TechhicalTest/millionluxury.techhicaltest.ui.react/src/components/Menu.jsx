import { useState } from 'react';
import { MENU_DATA } from '../data/menu-data';

export default function Menu({onSelect}) {
	const [selectedOption, setSelectedOption] = useState('welcome');

	function handleMenuSelect(option) {
		setSelectedOption(option);
		onSelect(option);
	}

	return (
		<div className="offcanvas offcanvas-start customBackGround" tabIndex="-1" id="offcanvasNavbarLight" aria-labelledby="offcanvasNavbarLightLabel" data-bs-backdrop="static">
			<div className="offcanvas-header">
				<h5 className="offcanvas-title" id="offcanvasNavbarLightLabel">Choose one option</h5>
				<button type="button" className="btn-close btn-close-white" data-bs-dismiss="offcanvas" data-bs-target="#offcanvasNavbarLight" aria-label="Close"></button>
			</div>
			<div className="offcanvas-body">
				<ul className="navbar-nav justify-content-end flex-grow-1 pe-3">
					{MENU_DATA.map((menuItem) => (
						<li key={menuItem.id} className="nav-item">
							<button 
								className={`btn btn btn-outline-light border-0 w-100 text-start ${selectedOption === menuItem.id ? 'customItemMenuDisabled' : 'customItemMenu'}`}
								disabled={selectedOption === menuItem.id}
								data-bs-dismiss="offcanvas" 
								data-bs-target="#offcanvasNavbarLight"
								onClick={() => handleMenuSelect(menuItem.id)}>
								<div className="d-flex">
									<div className="p-1">
										<i className={`fs-4 ${menuItem.iconClass}`}></i>
									</div>
									<div className="p-1 align-content-center">{menuItem.text}</div>
								</div>
							</button>
						</li>					
					))}
				</ul>
			</div>
		</div>
	);
}