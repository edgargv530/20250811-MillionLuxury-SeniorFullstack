import reactLogo from '../assets/react.svg'

export default function Footer() {
	return (
		<footer className="d-flex flex-wrap justify-content-between align-items-center py-3 my-4 border-top">
			<p className="mb-0 text-body-secondary">&copy; Edgar Gonzalez</p>
			<div className="d-flex align-items-center justify-content-center">
				<img src={reactLogo} alt="React image" width="48" height="48" />
			</div>
			<ul className="nav justify-content-end">
				<li className="nav-item">Version 20250813</li>
			</ul>
		</footer>
	);
}