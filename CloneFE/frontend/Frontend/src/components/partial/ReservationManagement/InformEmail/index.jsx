import React from 'react';
import styles from "./style.module.scss";
import { Link } from 'react-router-dom';

function InformEmail({ onBackClick, onSendClick }) {
    return (
        <div className={styles.container}>
            <div className={styles.table}>
                <h2 className={styles.header}>Email Preview</h2>
                <div className={styles.notification}>
                <div className={styles.columns}>
                        <div className={styles.column}>
                            <div className={styles.row}>
                                <span className={styles.label}>Template Name:</span>
                                <span className={styles.content}></span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>From:</span>
                                <span className={styles.content}>Phat(phatnhse171208@fpt.edu.vn)</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>To:</span>
                                <span className={styles.content}></span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Cc:</span>
                                <span className={styles.content}>phatnguyenhoang69@gmail.com</span>
                            </div>
                            <div className={styles.row}>
                                <span className={styles.label}>Subject:</span>
                                <span className={styles.content}>Fresher Academy nhớ bạn, [Tên học viên] ơi</span>
                            </div>
                        </div>
                        <div className={styles.column}>
                            <div className={styles.row}>
                                <span className={styles.label}>Body:</span>
                                <span className={styles.content} style={{ fontSize: "16px" }}>
                            <p>Chào [Tên Học Viên],</p>
                            <p>Chúc bạn một ngày tốt lành! Tôi là Phat, Admin của Fresher Academy.</p>
                            <p>Chúng tôi rất vui vì bạn đã quyết định bảo lưu tại Fresher Academy để giữ vững kiến thức và kỹ năng của mình. Hiện tại, tôi muốn nhắc nhở bạn rằng thời hạn bảo lưu của bạn sẽ kết thúc trong vòng một tháng nữa.</p>
                            <p>Để giúp bạn dễ dàng quay lại và tiếp tục hành trình học tập, chúng tôi đề xuất bạn nên liên hệ với chúng tôi trước khi thời hạn bảo lưu hết hạn. Bằng cách này, chúng tôi có thể hỗ trợ bạn trong quá trình xếp lớp và đảm bảo rằng bạn sẽ có trải nghiệm học tập tốt nhất tại Fresher Academy.</p>
                            <p>Hãy liên hệ với chúng tôi qua số điện thoại: [Số Điện Thoại của Bạn]. Chúng tôi sẽ sẵn lòng hỗ trợ bạn với mọi thắc mắc và yêu cầu của bạn.</p>
                            <p>Chân thành cảm ơn sự quan tâm và cam kết của bạn đối với chương trình học tại Fresher Academy. Chúng tôi mong sớm được gặp lại bạn và chia sẻ những kiến thức mới.</p>
                            <p>Trân trọng,</p>
                            <p>[Phat]</p>
                            <p>Admin Fresher Academy</p>
                        </span>
                        </div>
                        </div>
                    </div>
                    </div>
                <div className={styles.buttons}>
                    <button className={styles.button_back} onClick={onBackClick}><Link to="/reservation-management">Back</Link></button>
                    <button className={styles.button_send} onClick={onSendClick}>Send</button>
                </div>
            </div>
        </div>
    );
}

export default InformEmail;