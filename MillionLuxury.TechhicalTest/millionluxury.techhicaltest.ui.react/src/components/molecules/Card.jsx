export default function Card({ title, children }) {
	return (
		<div className="card">
			<div className="card-header">{title}</div>
			<div className="card-body">
				<div className="row">
					{children}
				</div>
			</div>
		</div >
	);
}