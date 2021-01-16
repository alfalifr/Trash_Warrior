using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class KinematicObject : MonoBehaviour
    {
        public LayerMask ground;
        public LayerMask collictible;
        public LayerMask water;
        public CompositeCollider2D waterC;

        protected Rigidbody2D rb; //{ get;  set => gravityScale = value.gravityScale; }
        protected Collider2D coll;
        protected Animator anim;

        protected readonly float _initSpeed = .2f;
        protected readonly float _maxSpeed = 7;
        protected readonly float _speed = 0.07f;
        protected readonly float _speedJump = 6;
        protected readonly float _gravityScale = 1;

        protected readonly float waterGravityScale = .11f;
        protected readonly float waterInitSpeed = .2f;
        protected readonly float waterMaxSpeed = 2.4f;
        protected readonly float waterSpeed = 0.07f;
        protected readonly float waterSpeedJump = 1.8f;

        protected float initSpeed = .2f;
        protected float maxSpeed = 7;
        protected float speed = 0.07f;
        protected float speedJump = 6;

        protected int jumpCount = 0;
        protected int jumpCountLimit = 2;

        protected Vector2 velocity;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            coll= GetComponent<Collider2D>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                velocity.x = initSpeed;
                anim.SetBool("running", true);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                velocity.x = 0;
                move(0);
                anim.SetBool("running", false);
                //rb.AddForce(new Vector2(-.0003f, rb.velocity.y));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //print("halo");
                accelerate();
                move(-velocity.x);
                transform.localScale = new Vector2(-1, 1);
            };

            if (Input.GetKey(KeyCode.RightArrow))
            {
                accelerate();
                move(velocity.x);
                transform.localScale = new Vector2(1, 1);
            };
            /*
            if (rb.velocity.y > 0){
                anim.SetBool("jumping", true);
            } else {
                if (rb.IsTouchingLayers(ground)) {
                    jumpCount = 0;
                    anim.SetBool("ground", true);
                } else {
                    anim.SetBool("ground", false);
                }
                anim.SetBool("jumping", false);
            }
            */
            if (rb.IsTouchingLayers(ground))
            {
                jumpCount = 0;
                anim.SetBool("ground", true);
            }
            else
            {
                anim.SetBool("jumping", rb.velocity.y > 0);
                //print("rb.velocity.y= " + rb.velocity.y);
                anim.SetBool("ground", false);
            }

            //print("isInWater= " + isInWater + " rb.IsTouchingLayers(water)= " + rb.IsTouchingLayers(water) + " water= " + water + " rb.IsTouching(waterC)= " + rb.IsTouching(waterC) + " waterC.gameObject.tag= " + waterC.gameObject.tag);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpCount < jumpCountLimit || jumpCountLimit < 0)
                {
                    //anim.SetBool("jumping", true);
                    rb.velocity = new Vector2(rb.velocity.x, speedJump);
                    jumpCount++;
                    anim.SetBool("ground", true);
                    anim.SetBool("jumping", rb.velocity.y > 0);
                }
            }
        }

        void accelerate(bool forward = true)
        {
            var newVal = velocity.x + velocity.x * speed;
            if (!forward) newVal *= -1;
            var x = Mathf.Min(newVal, maxSpeed);
            //print("acc x= " +x);
            velocity.x = x;
        }

        void move(float? x = null, float? y = null)
        {
            float x_ = x ?? rb.velocity.x;
            float y_ = y ?? rb.velocity.y;
            //print("x_= " +x_ +" y_= " +y_);
            rb.velocity = new Vector2(x_, y_);
        }

/*
        void OnTriggerStay2D(Collider2D hit)
        {
            if (hit.gameObject.tag == "Water")
            {
                rb.gravityScale = waterGravityScale;
                initSpeed = waterInitSpeed;
                speed = waterSpeed;
                maxSpeed = waterMaxSpeed;
                jumpCountLimit = -1;
                //print("maxSpeed stay= " + maxSpeed);
            }
        }
*/
        protected void OnTriggerEnter2D(Collider2D hit)
        {
            if (hit.gameObject.tag == "Water")
            {
                rb.gravityScale = waterGravityScale;
                initSpeed = waterInitSpeed;
                speed = waterSpeed;
                maxSpeed = waterMaxSpeed;
                speedJump = waterSpeedJump;
                jumpCountLimit = -1;
            }
        }

        protected void OnTriggerExit2D(Collider2D hit)
        {
            //print(hit.gameObject.tag);
            if (hit.gameObject.tag == "Water")
            {
                //var wb2 = hit.bounds;
                var cb = coll.bounds;

               
                //print("cb.extents.x < wb.min.x => " + (cb.extents.x < wb.min.x) + " cb.min.y > wb.max.y => " + (cb.min.y > wb.max.y) + " cb.min.x > wb.max.x => " + (cb.min.x > wb.max.x) + " cb.max.y < wb.min.y => " + (cb.max.y < wb.min.y));
                //print("cb.max= " + cb.max + " cb.min= " + cb.min + " cb.extents= " + cb.extents + " cb.size= " + cb.size);
                //print("wb2.max= " + wb2.max + " wb2.min= " + wb2.min + " wb2.extents= " + wb2.extents + " wb2.size= " + wb2.size + " wb2.center= " + wb2.center);
                //print("bc.bounds.center= " + bc.bounds.center);
                //print("hit.bounds.Contains(transform.position)= " + hit.bounds.Contains(transform.position));
                //print("hit.OverlapPoint(bc.bounds.center)= " + hit.OverlapPoint(cb.center) + " waterC.OverlapPoint(cb.center)= " + waterC.OverlapPoint(cb.center) + " waterC.ClosestPoint(cb.center)= " + waterC.ClosestPoint(cb.center));
                //print("(cb.max.x < wb2.min.x)= " + (cb.max.x < wb2.min.x) + " (cb.min.x > wb2.max.x)= " + (cb.min.x > wb2.max.x) + " (cb.min.y > wb2.max.y)= " + (cb.min.y > wb2.max.y) + " (cb.max.y < wb2.min.y)= " + (cb.max.y < wb2.min.y));

                //var waterPerimeter = waterC.ClosestPoint(cb.center);
                if (isInside(waterC))
                {
                    print("OnTriggerExit2D di luar air bro!!!");
                    rb.gravityScale = _gravityScale;
                    initSpeed = _initSpeed;
                    speed = _speed;
                    maxSpeed = _maxSpeed;
                    speedJump = _speedJump;
                    jumpCountLimit = 2;
                }
                //print("cb.max= " + );
                //print("cb.extents.x < wb.min.x => " + (cb.extents.x < wb.min.x) + " cb.min.y > wb.max.y => " + (cb.min.y > wb.max.y) + " cb.min.x > wb.max.x => " + (cb.min.x > wb.max.x) + " cb.max.y < wb.min.y => " + (cb.max.y < wb.min.y));
                //print("maxSpeed= " + maxSpeed);
            }
        }

        protected void OnCollisionEnter(Collision collision)
        {
            print("collsision enter tag= " + collision.gameObject.tag);
        }

        public bool isInside(Collider2D obj) {
            var cb = coll.bounds;
            var perimeter = obj.ClosestPoint(cb.center);
            return cb.max.x < perimeter.x || cb.min.x > perimeter.x || cb.min.y > perimeter.y || cb.max.y < perimeter.y;
        }
    }
}
